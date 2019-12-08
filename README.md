# Warehouse Automation
Warehouse automation using Windows CE bar-code scanner devices.
Client: C# WinForms application for WinCE device
Server: MS SQL Server

## Parameters

1 // Device unique number
Server=;User ID=;Password=;Initial Catalog=Store; // DB connection string
50 // thread synchronization time interval, scanned data transfer from one thread to another one
10000 // time inteval to poll SQL server
30 // wait time to execute SQL command
240;320 // WinCE device screen size

## Terminal program

1. Check options in Terminal.config.txt.
2. The computer or device should be conected to local network.

## Hot keys

'e' on DocumentForm - emulates scan event.

## Emulator

Symptom: When the Emulator is being started, a message box appears titled "Emulator for Windows CE" with the text "One or more files from the Emulator for Windows CE installation is missing. Please reinstall Emulator for Windows CE and try again." The connection or deployment to the Emulator will fail after the message box. 

Cause: A typical cause is that the Emulator driver is not installed or is corrupted. 

Workaround: Go to "Device Manager" (Right-click on "My Computer", select Properties->Hardware and press the "Device Manager" button) and check whether "Virtual PC Application Services" is installed under the System Devices group. If the driver is not installed, install it by running 
"<VSROOT>\CompactFrameworkSDK\ConnectionManager\Bin\DriverInstall\Smart Devices Emulator.msi". 

Note: Verify that the "Virtual PC Application Services" appears after running the msi file. If not then reboot the PC and attempt the installation again. 

## ActiveSync

- install ActiveSync 4.2
- connect device via usb cable
- start the program
- it should establish the connection and ask to setup a partnership

Set up a partnership
- set up a Partnership - set Yes
- No I want to sync with 2 computers
- Sync settings - uncheck all

## Known Issues

Issue: There was a problem with ActiveSync, it didn't work properly. 
Fix: I disconnected usb cable, disconnected local network connection and exited firewall. Then AS was reinstalled ok.

Issue: Debugger not working.
Fix: To debug with real device you should clck Build Cab File button at first.

Issue: Specified SQL server not found
Fix: reinstall sql server 2000, set nonamed instance, install sp3 for sql server, check firewall.

Issue: Вылезла проблема - не могу побороть, кладовщик промухал и половина терминалов разрядилось, тепрь программа на них не запускается никак, чтобы я не делал
Fix: скопировал файл экзе + файл Терминал.Апи.Длл (хотя обычно этого не требовалось), потом переустановил русификатор.