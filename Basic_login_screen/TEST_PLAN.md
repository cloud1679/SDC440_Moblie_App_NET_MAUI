# Basic Login Screen — Test Plan

**Project:** SDC440 Mobile Application Development / Basic_login_screen  
**Prepared for:** Richard Burns  
**Date:** October 3, 2026  
**Plan version:** 1.0  
**Application version:** 1.0 (build 1)  
**Status:** Ready for manual execution; no runtime tests have been performed for this plan.

## 1. Purpose and basis

Verify that the .NET MAUI application displays its login screen, checks the demo credentials, reports the correct result, and clears the form when Cancel is selected.

This plan is based on `Basic_login_screen/MainPage.xaml`, `MainPage.xaml.cs`, `AppShell.xaml`, and `Basic_login_screen.csproj`. Previous test plans were not found in the available repository or its document history, so this document uses a standalone manual test format. Expected functional results describe the current implementation; usability checks describe acceptance goals requiring runtime verification.

## 2. Scope

Included: initial screen, username and password entry, password masking, successful and failed login messages, empty and unusual input, repeated attempts, Cancel, offline operation, layout, keyboard use, and accessibility checks.

Excluded: account registration, password recovery, database or web-service authentication, account lockout, session management, and navigation after login. These features are not implemented in this application.

The app compares input locally against username `Burns` and password `Password1`. Matching is case-sensitive, and input is not trimmed. Success only updates the message on the same screen. These hard-coded credentials are for this demonstration; this plan does not establish production authentication security.

## 3. Test environment and preparation

Use a compatible .NET 10 SDK, MAUI workload, and platform build tools. Build and install the application on each platform selected for the test cycle. Record exact SDK, workload, OS, device or simulator, and application revision before execution.

| Configured target | Minimum OS declared by project | Execution environment | Status |
| --- | --- | --- | --- |
| Android | API 21 / Android 5.0 | Physical device or emulator | Not run |
| iOS | iOS 15.0 | Physical device or simulator with Apple tooling | Not run |
| Mac Catalyst | macOS 15.0 platform version | Compatible Mac | Not run |
| Windows | Windows 10 build 17763 | Windows machine | Not run |

The values above are project declarations, not a guarantee that the installed toolchain supports every OS version. Execute the full functional suite on each selected target. Run touch and software-keyboard checks on mobile and keyboard/window-resizing checks on desktop. Identify any omitted target and the reason in the final report.

Before each independent test, launch the application and select Cancel to clear the form unless the case specifies a fresh launch or a multi-step sequence. Use only demo test data. In the cases below, `""` means empty input and quotation marks around sample values are not typed. Quoted message strings preserve significant trailing spaces.

## 4. Manual test cases

Priority: **High** = core login, clearing, or password display behavior; **Medium** = boundary or usability coverage. Every case starts as **Not run**. Record actual results separately for each platform using the execution record in section 5.

| ID | Priority | Test / steps | Expected result | Status |
| --- | --- | --- | --- | --- |
| TC-01 | High | Fully close and launch the app. Inspect the screen. | Shows Richard Burns, Login Screen, User Name, Password, username and password entries, and Login and Cancel buttons. Entries and result message are empty; placeholders are visible. | Not run |
| TC-02 | High | Enter username `Burns` and password `Password1`; select Login. | Message is exactly `"Login successful Burns"`. The app stays on the login screen and entered values remain. | Not run |
| TC-03 | High | Enter `Burns` / `WrongPassword`; select Login. | Message is exactly `"Login failed Burns"`. No successful login is reported. | Not run |
| TC-04 | High | Enter `OtherUser` / `Password1`; select Login. | Message is exactly `"Login failed OtherUser"`. | Not run |
| TC-05 | High | Enter `OtherUser` / `WrongPassword`; select Login. | Message is exactly `"Login failed OtherUser"`. | Not run |
| TC-06 | High | On a fresh launch, leave both fields untouched; select Login. Repeat after selecting Cancel. | Both attempts show `"Login failed "` (one trailing space). No crash occurs for untouched or cleared entries. | Not run |
| TC-07 | High | Leave username empty; enter `Password1`; select Login. | Message is `"Login failed "`. | Not run |
| TC-08 | High | Enter `Burns`; leave password empty; select Login. | Message is `"Login failed Burns"`. | Not run |
| TC-09 | Medium | Test `burns` / `Password1`, then `BURNS` / `Password1`, selecting Login after each pair. | Both fail; messages are `"Login failed burns"` and `"Login failed BURNS"`, respectively. | Not run |
| TC-10 | Medium | Test `Burns` / `password1`, then `Burns` / `PASSWORD1`. | Both show `"Login failed Burns"`. | Not run |
| TC-11 | Medium | Test username `" Burns"`, then `"Burns "`, with password `Password1`. | Both fail. The message includes the username exactly as entered, including its leading or trailing space. | Not run |
| TC-12 | Medium | Test `Burns` with password `" Password1"`, then `"Password1 "`. | Both show `"Login failed Burns"`; password spaces are not trimmed. | Not run |
| TC-13 | Medium | Enter three spaces in each field; select Login. | Login fails without a crash. Message is `"Login failed    "` (four spaces after `failed`, including the separator). | Not run |
| TC-14 | High | Type and paste `Password1` into the password entry; inspect it while focused and unfocused. Select Login. | Password uses the platform's masked-entry behavior and is not rendered as a readable full string or included in the result message. Brief native last-character reveal, if present, is recorded. | Not run |
| TC-15 | High | Enter text in both fields without logging in; select Cancel. | Both entries and the result message are empty. The app remains open on the login screen. | Not run |
| TC-16 | High | Complete a successful login; select Cancel. | Username, password, and success message are cleared. | Not run |
| TC-17 | High | Complete a failed login; select Cancel. | Username, password, and failure message are cleared. | Not run |
| TC-18 | Medium | With an empty form, select Cancel three times. | Form remains empty; no error or crash occurs. | Not run |
| TC-19 | High | Submit `Burns` / `WrongPassword`; replace the password with `Password1`; select Login again. | Failure message is replaced with `"Login successful Burns"`. | Not run |
| TC-20 | High | Submit valid credentials; replace the password with `WrongPassword`; select Login again. | Success message is replaced with `"Login failed Burns"`. | Not run |
| TC-21 | Medium | Enter valid credentials; select Login five times in succession. | Same success message remains; no duplicate UI, navigation, or crash occurs. | Not run |
| TC-22 | Medium | Enter username `Tést用户!@#` and password `abc!@#`; select Login. | Message is `"Login failed Tést用户!@#"`; text is handled without a crash. | Not run |
| TC-23 | Medium | Paste 256 `A` characters into username and 256 `B` characters into password; select Login, then Cancel. | Login fails without a crash, and Cancel clears the fields and message. Record any clipped message or inaccessible controls as a usability issue. | Not run |
| TC-24 | Medium | Disable network access and submit valid credentials, then invalid credentials. Restore network access afterward. | Valid credentials succeed and invalid credentials fail with the same messages as online operation. | Not run |
| TC-25 | Medium | On mobile, focus each entry with the software keyboard visible. Test available portrait/landscape layouts. On desktop, resize to a small usable window. | Fields and buttons remain reachable, using scrolling as needed; labels and controls do not overlap. Record tested sizes/orientations. | Not run |
| TC-26 | Medium | With a hardware keyboard, move through interactive controls with Tab/Shift+Tab and activate buttons using platform-standard keys. | Focus is visible and follows a usable order; both entries and buttons can be operated without a pointer. | Not run |
| TC-27 | Medium | Inspect the screen in light and dark modes and with larger system text. | Labels, entries, result messages, and buttons remain readable and reachable without overlapping content. | Not run |
| TC-28 | Medium | Enable the platform screen reader; navigate the form, enter credentials, and submit a login. | Controls have understandable names and roles; password content is protected; the login result can be discovered and read. Record any missing labels or result feedback as accessibility issues. | Not run |

## 5. Execution and defect records

Create one execution record for each case on each tested platform. Do not mark a case Pass based only on source inspection.

| Run date | Tester | App revision | Platform / OS / device | Case ID | Actual result | Pass / Fail / Blocked / Not run | Evidence / defect ID |
| --- | --- | --- | --- | --- | --- | --- | --- |
| — | — | — | — | — | — | Not run | — |

For each failure, record a defect ID, affected environment, reproduction steps, expected and actual results, severity, and screenshot or recording where useful. Retest corrected defects and repeat TC-02, TC-03, and TC-15 through TC-20 after changes to login or clearing behavior.

## 6. Entry and completion criteria

Testing begins when the selected platform builds and launches, the environment is recorded, and the tester can access the form. A build or installation failure is recorded as a blocker, not as a passing or executed UI test.

The cycle is complete when all planned cases have been executed on the selected platforms, all High-priority cases pass, no unresolved crash or core-functional defect remains, and remaining usability issues have a documented disposition. Explicitly list any untested platform, blocked case, or accepted limitation before sign-off.

## 7. Test summary and sign-off

**Current result:** Planning and source review only. No runtime pass/fail claims are made.

| Summary item | Result |
| --- | --- |
| Defined cases | 28 |
| Executed cases | 0 |
| Passed / Failed / Blocked | 0 / 0 / 0 |
| Not run | 28 per planned platform |
| Platforms tested | None |
| Open defects | Not assessed |
| Tester / completion date | Pending |
| Reviewer / approval date | Pending |
