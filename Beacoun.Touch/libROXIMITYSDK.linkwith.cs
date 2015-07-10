using System;
using ObjCRuntime;

[assembly: LinkWith ("libROXIMITYSDK.a", LinkTarget.Simulator | LinkTarget.ArmV7 | LinkTarget.Simulator64 | LinkTarget.Arm64, 
	SmartLink = true, ForceLoad = true, 
	Frameworks = "AdSupport SystemConfiguration Security CoreTelephony MobileCoreServices CoreLocation CoreBluetooth",
	LinkerFlags = "-lz -lsqlite3 -ObjC")]
