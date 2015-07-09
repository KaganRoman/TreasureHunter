using System;

using CoreBluetooth;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using UIKit;
using CoreGraphics;

namespace Beacoun.Touch
{
	// @protocol ROXBeaconRangeUpdateDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface ROXBeaconRangeUpdateDelegate
	{
		// @required -(void)didUpdateBeaconRanges:(NSArray *)rangedBeacons;
		[Abstract]
		[Export ("didUpdateBeaconRanges:")]
	//	[Verify (StronglyTypedNSArray)]
		void DidUpdateBeaconRanges (NSObject[] rangedBeacons);
	}

	// @protocol ROXDeviceHookDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface ROXDeviceHookDelegate
	{
		// @required -(void)handleDeviceHook:(NSDictionary *)actionDictionary andTrigger:(NSDictionary *)triggerDictionary isInBackground:(BOOL)backgrounded;
		[Abstract]
		[Export ("handleDeviceHook:andTrigger:isInBackground:")]
		void AndTrigger (NSDictionary actionDictionary, NSDictionary triggerDictionary, bool backgrounded);
	}

	// @protocol ROXIMITYEngineDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface ROXIMITYEngineDelegate
	{
		// @optional -(void)bluetoothRoximityUsable:(BOOL)usable state:(CBCentralManagerState)state;
		[Export ("bluetoothRoximityUsable:state:")]
		void BluetoothRoximityUsable (bool usable, CBCentralManagerState state);

		// @optional -(void)locationRoximityUsable:(BOOL)usable status:(CLAuthorizationStatus)authStatus;
		[Export ("locationRoximityUsable:status:")]
		void LocationRoximityUsable (bool usable, CLAuthorizationStatus authStatus);

		// @optional -(void)notificationsRoximityPermitted:(BOOL)permitted;
		[Export ("notificationsRoximityPermitted:")]
		void NotificationsRoximityPermitted (bool permitted);

		// @optional -(void)aliasSetResult:(BOOL)success alias:(NSString *)alias error:(NSError *)error;
		[Export ("aliasSetResult:alias:error:")]
		void AliasSetResult (bool success, string alias, NSError error);

		// @optional -(void)aliasRemoveResult:(BOOL)success error:(NSError *)error;
		[Export ("aliasRemoveResult:error:")]
		void AliasRemoveResult (bool success, NSError error);
	}

	// @interface ROXIMITYEngine : NSObject
	[BaseType (typeof(NSObject))]
	interface ROXIMITYEngine
	{
		// +(void)startWithLaunchOptions:(NSDictionary *)launchOptions andEngineOptions:(NSDictionary *)engineOptions __attribute__((deprecated("use startWithLaunchOptions:engineOptions:applicationId:andEngineDelegate instead")));
		[Static]
		[Export ("startWithLaunchOptions:andEngineOptions:")]
		void StartWithLaunchOptions (NSDictionary launchOptions, NSDictionary engineOptions);

		// +(void)startWithLaunchOptions:(NSDictionary *)launchOptions engineOptions:(NSDictionary *)engineOptions andApplicationId:(NSString *)applicationId __attribute__((deprecated("use startWithLaunchOptions:engineOptions:applicationId:andEngineDelegate instead")));
		[Static]
		[Export ("startWithLaunchOptions:engineOptions:andApplicationId:")]
		void StartWithLaunchOptions ([NullAllowed] NSDictionary launchOptions, [NullAllowed] NSDictionary engineOptions, string applicationId);

		// +(void)startWithLaunchOptions:(NSDictionary *)launchOptions engineOptions:(NSDictionary *)engineOptions applicationId:(NSString *)applicationId andEngineDelegate:(id<ROXIMITYEngineDelegate>)engineDelegate;
		[Static]
		[Export ("startWithLaunchOptions:engineOptions:applicationId:andEngineDelegate:")]
		void StartWithLaunchOptions ([NullAllowed] NSDictionary launchOptions, [NullAllowed] NSDictionary engineOptions, string applicationId, [NullAllowed] ROXIMITYEngineDelegate engineDelegate);

		// +(void)active;
		[Static]
		[Export ("active")]
		void Active ();

		// +(void)resignActive;
		[Static]
		[Export ("resignActive")]
		void ResignActive ();

		// +(void)background;
		[Static]
		[Export ("background")]
		void Background ();

		// +(void)foreground;
		[Static]
		[Export ("foreground")]
		void Foreground ();

		// +(void)terminate;
		[Static]
		[Export ("terminate")]
		void Terminate ();

		// +(void)setAlias:(NSString *)alias;
		[Static]
		[Export ("setAlias:")]
		void SetAlias (string alias);

		// +(void)removeAlias;
		[Static]
		[Export ("removeAlias")]
		void RemoveAlias ();

		// +(NSString *)getAlias;
		[Static]
		[Export ("getAlias")]
	//	[Verify (MethodToProperty)]
		string Alias { get; }

		// +(void)setROXIMITYEngineDelegate:(id<ROXIMITYEngineDelegate>)roximityEngineDelegate;
		[Static]
		[Export ("setROXIMITYEngineDelegate:")]
		void SetROXIMITYEngineDelegate (ROXIMITYEngineDelegate roximityEngineDelegate);

		// +(void)setBeaconRangeDelegate:(id<ROXBeaconRangeUpdateDelegate>)beaconRangeUpdateDelegate withUpdateInterval:(ROXBeaconRangeUpdateInterval)interval;
		[Static]
		[Export ("setBeaconRangeDelegate:withUpdateInterval:")]
		void SetBeaconRangeDelegate (ROXBeaconRangeUpdateDelegate beaconRangeUpdateDelegate, ROXBeaconRangeUpdateInterval interval);

		// +(void)setDeviceHookDelegate:(id<ROXDeviceHookDelegate>)deviceHookDelegate;
		[Static]
		[Export ("setDeviceHookDelegate:")]
		void SetDeviceHookDelegate (ROXDeviceHookDelegate deviceHookDelegate);

		// +(void)didRegisterForRemoteNotifications:(NSData *)deviceToken;
		[Static]
		[Export ("didRegisterForRemoteNotifications:")]
		void DidRegisterForRemoteNotifications (NSData deviceToken);

		// +(void)didFailToRegisterForRemoteNotifications:(NSError *)error;
		[Static]
		[Export ("didFailToRegisterForRemoteNotifications:")]
		void DidFailToRegisterForRemoteNotifications (NSError error);

		// +(void)deactivateLocation;
		[Static]
		[Export ("deactivateLocation")]
		void DeactivateLocation ();

		// +(void)activateLocation;
		[Static]
		[Export ("activateLocation")]
		void ActivateLocation ();

		// +(BOOL)isLocationActivated;
		[Static]
		[Export ("isLocationActivated")]
	//	[Verify (MethodToProperty)]
		bool IsLocationActivated { get; }

		// +(void)disableLocalNotifications;
		[Static]
		[Export ("disableLocalNotifications")]
		void DisableLocalNotifications ();

		// +(void)enableLocalNotifications;
		[Static]
		[Export ("enableLocalNotifications")]
		void EnableLocalNotifications ();

		// +(BOOL)isLocalNotificationEnabled;
		[Static]
		[Export ("isLocalNotificationEnabled")]
	//	[Verify (MethodToProperty)]
		bool IsLocalNotificationEnabled { get; }

		// +(void)addVisitMetadata:(NSString *)tag withValue:(NSObject *)value;
		[Static]
		[Export ("addVisitMetadata:withValue:")]
		void AddVisitMetadata (string tag, NSObject value);

		// +(void)addVisitMetadataDictionary:(NSDictionary *)visitMetadataDictionary;
		[Static]
		[Export ("addVisitMetadataDictionary:")]
		void AddVisitMetadataDictionary (NSDictionary visitMetadataDictionary);

		// +(void)addRegistrationMetadata:(NSString *)tag withValue:(NSObject *)value;
		[Static]
		[Export ("addRegistrationMetadata:withValue:")]
		void AddRegistrationMetadata (string tag, NSObject value);

		// +(void)addRegistrationMetadataDictionary:(NSDictionary *)registrationMetadataDictionary;
		[Static]
		[Export ("addRegistrationMetadataDictionary:")]
		void AddRegistrationMetadataDictionary (NSDictionary registrationMetadataDictionary);

		// +(CLLocation *)getMostRecentLocation;
		[Static]
		[Export ("getMostRecentLocation")]
	//	[Verify (MethodToProperty)]
		CLLocation MostRecentLocation { get; }

		// +(NSArray *)retrieveLastNBeacons:(int)numberOfBeacons;
		[Static]
		[Export ("retrieveLastNBeacons:")]
	//	[Verify (StronglyTypedNSArray)]
		NSObject[] RetrieveLastNBeacons (int numberOfBeacons);

		// +(NSArray *)retrieveBeaconsSeenInLastNSeconds:(int)seconds;
		[Static]
		[Export ("retrieveBeaconsSeenInLastNSeconds:")]
	//	[Verify (StronglyTypedNSArray)]
		NSObject[] RetrieveBeaconsSeenInLastNSeconds (int seconds);

		// +(void)reset;
		[Static]
		[Export ("reset")]
		void Reset ();
	}

	// @interface ROXNotifications : NSObject
	[BaseType (typeof(NSObject))]
	interface ROXNotifications
	{
	}

//	[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants
	{
		// extern NSString *const kROXEngineOptionsReservedRegions;
		[Field ("kROXEngineOptionsReservedRegions")]
		NSString kROXEngineOptionsReservedRegions { get; }

		// extern NSString *const kROXEngineOptionsMuteBluetoothOffAlert;
		[Field ("kROXEngineOptionsMuteBluetoothOffAlert")]
		NSString kROXEngineOptionsMuteBluetoothOffAlert { get; }

		// extern NSString *const kROXEngineOptionsMuteRequestAlerts;
		[Field ("kROXEngineOptionsMuteRequestAlerts")]
		NSString kROXEngineOptionsMuteRequestAlerts { get; }

		// extern NSString *const kROXEngineOptionsMuteLocationPermissionAlert;
		[Field ("kROXEngineOptionsMuteLocationPermissionAlert")]
		NSString kROXEngineOptionsMuteLocationPermissionAlert { get; }

		// extern NSString *const kROXEngineOptionsMuteNotificationPermissionAlert;
		[Field ("kROXEngineOptionsMuteNotificationPermissionAlert")]
		NSString kROXEngineOptionsMuteNotificationPermissionAlert { get; }

		// extern NSString *const kROXEngineOptionsStartLocationDeactivated;
		[Field ("kROXEngineOptionsStartLocationDeactivated")]
		NSString kROXEngineOptionsStartLocationDeactivated { get; }

		// extern NSString *const kROXEngineOptionsDebugLogging;
		[Field ("kROXEngineOptionsDebugLogging")]
		NSString kROXEngineOptionsDebugLogging { get; }

		// extern NSString *const kROXEngineOptionsVerboseLogging;
		[Field ("kROXEngineOptionsVerboseLogging")]
		NSString kROXEngineOptionsVerboseLogging { get; }

		// extern NSString *const kROXEngineOptionsUserTargetingLimited;
		[Field ("kROXEngineOptionsUserTargetingLimited")]
		NSString kROXEngineOptionsUserTargetingLimited { get; }

		// extern NSString *const kROXStateNotification;
		[Field ("kROXStateNotification")]
		NSString kROXStateNotification { get; }

		// extern NSString *const kROXRoximityLocalNotification;
		[Field ("kROXRoximityLocalNotification")]
		NSString kROXRoximityLocalNotification { get; }

		// extern NSString *const kROXRoximityPushNotification;
		[Field ("kROXRoximityPushNotification")]
		NSString kROXRoximityPushNotification { get; }

		// extern NSString *const kROXRoximityActionNotification;
		[Field ("kROXRoximityActionNotification")]
		NSString kROXRoximityActionNotification { get; }

		// extern NSString *const kROXRoximityTriggerNotification;
		[Field ("kROXRoximityTriggerNotification")]
		NSString kROXRoximityTriggerNotification { get; }

		// extern NSString *const kROXBeaconRangeUpdated;
		[Field ("kROXBeaconRangeUpdated")]
		NSString kROXBeaconRangeUpdated { get; }

		// extern NSString *const kROXMessageFired;
		[Field ("kROXMessageFired")]
		NSString kROXMessageFired { get; }

		// extern NSString *const kROXWebhookPosted;
		[Field ("kROXWebhookPosted")]
		NSString kROXWebhookPosted { get; }

		// extern NSString *const kROXKeyGeoRegionId;
		[Field ("kROXKeyGeoRegionId")]
		NSString kROXKeyGeoRegionId { get; }

		// extern NSString *const kROXKeyGeoRegionLat;
		[Field ("kROXKeyGeoRegionLat")]
		NSString kROXKeyGeoRegionLat { get; }

		// extern NSString *const kROXKeyGeoRegionLon;
		[Field ("kROXKeyGeoRegionLon")]
		NSString kROXKeyGeoRegionLon { get; }

		// extern NSString *const kROXKeyGeoRegionRadius;
		[Field ("kROXKeyGeoRegionRadius")]
		NSString kROXKeyGeoRegionRadius { get; }

		// extern NSString *const kROXKeyGeoRegionName;
		[Field ("kROXKeyGeoRegionName")]
		NSString kROXKeyGeoRegionName { get; }

		// extern NSString *const kROXKeyGeoRegionTags;
		[Field ("kROXKeyGeoRegionTags")]
		NSString kROXKeyGeoRegionTags { get; }

		// extern NSString *const kROXKeyBeaconId;
		[Field ("kROXKeyBeaconId")]
		NSString kROXKeyBeaconId { get; }

		// extern NSString *const kROXKeyBeaconName;
		[Field ("kROXKeyBeaconName")]
		NSString kROXKeyBeaconName { get; }

		// extern NSString *const kROXKeyBeaconTags;
		[Field ("kROXKeyBeaconTags")]
		NSString kROXKeyBeaconTags { get; }

		// extern NSString *const kROXKeyBeaconProximityValue;
		[Field ("kROXKeyBeaconProximityValue")]
		NSString kROXKeyBeaconProximityValue { get; }

		// extern NSString *const kROXKeyBeaconProximityString;
		[Field ("kROXKeyBeaconProximityString")]
		NSString kROXKeyBeaconProximityString { get; }

		// extern NSString *const ROX_NOTIF_BEACON_RANGE_UPDATE;
		[Field ("ROX_NOTIF_BEACON_RANGE_UPDATE")]
		NSString ROX_NOTIF_BEACON_RANGE_UPDATE { get; }

		// extern NSString *const ROX_NOTIF_REGION_ENTERED;
		[Field ("ROX_NOTIF_REGION_ENTERED")]
		NSString ROX_NOTIF_REGION_ENTERED { get; }

		// extern NSString *const ROX_NOTIF_REGION_EXITED;
		[Field ("ROX_NOTIF_REGION_EXITED")]
		NSString ROX_NOTIF_REGION_EXITED { get; }

		// extern NSString *const ROX_NOTIF_BLUETOOTH_OFF;
		[Field ("ROX_NOTIF_BLUETOOTH_OFF")]
		NSString ROX_NOTIF_BLUETOOTH_OFF { get; }

		// extern NSString *const ROX_NOTIF_MESSAGE_FIRED;
		[Field ("ROX_NOTIF_MESSAGE_FIRED")]
		NSString ROX_NOTIF_MESSAGE_FIRED { get; }

		// extern NSString *const ROX_BEACON_RANGE_KEY_BEACON_ID;
		[Field ("ROX_BEACON_RANGE_KEY_BEACON_ID")]
		NSString ROX_BEACON_RANGE_KEY_BEACON_ID { get; }

		// extern NSString *const ROX_BEACON_RANGE_KEY_BEACON_NAME;
		[Field ("ROX_BEACON_RANGE_KEY_BEACON_NAME")]
		NSString ROX_BEACON_RANGE_KEY_BEACON_NAME { get; }

		// extern NSString *const ROX_BEACON_RANGE_KEY_BEACON_TAGS;
		[Field ("ROX_BEACON_RANGE_KEY_BEACON_TAGS")]
		NSString ROX_BEACON_RANGE_KEY_BEACON_TAGS { get; }

		// extern NSString *const ROX_BEACON_RANGE_KEY_PROXIMITY_VALUE;
		[Field ("ROX_BEACON_RANGE_KEY_PROXIMITY_VALUE")]
		NSString ROX_BEACON_RANGE_KEY_PROXIMITY_VALUE { get; }

		// extern NSString *const ROX_BEACON_RANGE_KEY_PROXIMITY_STRING;
		[Field ("ROX_BEACON_RANGE_KEY_PROXIMITY_STRING")]
		NSString ROX_BEACON_RANGE_KEY_PROXIMITY_STRING { get; }

		// extern NSString *const kROXNotifBeaconId;
		[Field ("kROXNotifBeaconId")]
		NSString kROXNotifBeaconId { get; }

		// extern NSString *const kROXNotifBeaconName;
		[Field ("kROXNotifBeaconName")]
		NSString kROXNotifBeaconName { get; }

		// extern NSString *const kROXNotifBeaconTags;
		[Field ("kROXNotifBeaconTags")]
		NSString kROXNotifBeaconTags { get; }

		// extern NSString *const kROXNotifBeaconProximityValue;
		[Field ("kROXNotifBeaconProximityValue")]
		NSString kROXNotifBeaconProximityValue { get; }

		// extern NSString *const kROXNotifBeaconProximityString;
		[Field ("kROXNotifBeaconProximityString")]
		NSString kROXNotifBeaconProximityString { get; }

		// extern NSString *const kROXNotifGeoRegionId;
		[Field ("kROXNotifGeoRegionId")]
		NSString kROXNotifGeoRegionId { get; }

		// extern NSString *const kROXNotifGeoRegionLat;
		[Field ("kROXNotifGeoRegionLat")]
		NSString kROXNotifGeoRegionLat { get; }

		// extern NSString *const kROXNotifGeoRegionLon;
		[Field ("kROXNotifGeoRegionLon")]
		NSString kROXNotifGeoRegionLon { get; }

		// extern NSString *const kROXNotifGeoRegionRadius;
		[Field ("kROXNotifGeoRegionRadius")]
		NSString kROXNotifGeoRegionRadius { get; }

		// extern NSString *const kROXNotifGeoRegionName;
		[Field ("kROXNotifGeoRegionName")]
		NSString kROXNotifGeoRegionName { get; }

		// extern NSString *const kROXNotifGeoRegionTags;
		[Field ("kROXNotifGeoRegionTags")]
		NSString kROXNotifGeoRegionTags { get; }
	}

}

