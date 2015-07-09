using System;
using ObjCRuntime;

[assembly: LinkWith ("libROXIMITYSDK.a", LinkTarget.Simulator | LinkTarget.ArmV7 | LinkTarget.Simulator64 | LinkTarget.Arm64, 
	SmartLink = true, ForceLoad = true, Frameworks = "AdSupport SystemConfiguration CoreTelephony MobileCoreServices")]
