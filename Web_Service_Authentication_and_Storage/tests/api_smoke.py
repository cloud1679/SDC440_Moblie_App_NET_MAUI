"""Run after dotnet build. Uses a temporary SQLite database, leaving real items intact."""
import base64
import json
import os
from pathlib import Path
import socket
import subprocess
import tempfile
import time
import urllib.error
import urllib.request

root = Path(__file__).resolve().parents[1]
with socket.socket() as sock:
    sock.bind(('127.0.0.1', 0))
    port = sock.getsockname()[1]
url = f'http://127.0.0.1:{port}'

def request(path, body=None, auth=None):
    headers = {}
    if auth is not None:
        headers['Authorization'] = auth
    if body is not None:
        headers['Content-Type'] = 'application/json'
    req = urllib.request.Request(url + path,
        data=None if body is None else json.dumps(body).encode(), headers=headers)
    try:
        response = urllib.request.urlopen(req, timeout=3)
    except urllib.error.HTTPError as error:
        response = error
    with response:
        data = response.read()
        return response.status, json.loads(data) if data else None, response.headers

def basic(value):
    return 'Basic ' + base64.b64encode(value.encode()).decode()

with tempfile.TemporaryDirectory() as temp:
    env = dict(os.environ, ASPNETCORE_ENVIRONMENT='Development',
               ASPNETCORE_URLS=url, DatabasePath=str(Path(temp) / 'test.db3'))
    def start():
        process = subprocess.Popen(['dotnet', str(root / 'bin/Debug/net10.0/Web_Service_Authentication_and_Storage.dll')],
                                   cwd=root, env=env, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
        for _ in range(100):
            try:
                if request('/api/items')[0] == 200:
                    return process
            except (OSError, urllib.error.URLError):
                pass
            if process.poll() is not None:
                raise RuntimeError('API exited during startup')
            time.sleep(.1)
        process.terminate()
        raise RuntimeError('API failed to start')
    process = start()
    try:
        for auth in [None, basic('Burns01:wrong'), basic('wrong:Password1'), basic('burns01:Password1'),
                     'Basic !!!', 'Bearer token', basic('Burns01:Password1:extra')]:
            status, _, headers = request('/api/auth/login', auth=auth)
            assert status == 401, (auth, status)
            assert headers['WWW-Authenticate'].startswith('Basic ')
        assert request('/api/auth/login', auth=basic('Burns01:Password1'))[0] == 200
        assert request('/api/items')[1] == []
        for missing in ['itemId', 'itemName', 'itemDescription']:
            item = dict(itemId='A1', itemName='Notebook', itemDescription='Blue cover')
            item[missing] = '   '
            assert request('/api/items', item)[0] == 400
            item[missing] = None
            assert request('/api/items', item)[0] == 400
        item = dict(itemId='A1', itemName='Notebook', itemDescription='Blue cover')
        assert request('/api/items', item)[0] == 201
        assert request('/api/items', item)[0] == 409
        assert request('/api/items')[1] == [item]
    finally:
        process.terminate()
        process.wait(timeout=10)
    process = start()
    try:
        assert request('/api/items')[1] == [item], 'Items must survive API restart'
    finally:
        process.terminate()
        process.wait(timeout=10)
print('PASS: valid/invalid/malformed Basic auth, anonymous data access, validation, duplicates, retrieval, persistence')
