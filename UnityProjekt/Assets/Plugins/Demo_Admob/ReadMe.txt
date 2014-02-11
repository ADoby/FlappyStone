Welcome to the Plato Evolved AdMob Android Unity Plugin v1.10

Setup

1 Download the asset from the Asset store (or import the package)
2 Amend the AndroidManifest.xml file with your AdMob Ad unit ID
3 Deploy your game to an android device to check that you see the ad
4 Done!

In Detail

The AndroidManifest.xml file is in the Plugins/Android folder

The line you need to amend in the AndroidManifest.xml file is:

    <meta-data android:name="admob_pub_id" android:value="Enter AdMob Ad unit ID here"/>


Change it to look something like (i.e. put in your Ad unit ID that you get from signing up with AdMob):

    <meta-data android:name="admob_pub_id" android:value="ca-app-pub-123456789/123456789"/>



Scripting

Without doing any scripting, the default position of the ad banner can be set in the AndroidManifest.xml file:

    <meta-data android:name="adposition_x" android:value="middle"/>
    <meta-data android:name="adposition_y" android:value="bottom"/>

The possible values of adposition_x are: left,middle or right	
The possible values of adposition_y are: top or bottom	

If you want to change the position (or hide/show) the ad banner during your game check out the included DemoScene but here is a quick example:

AdMobUnityPlugin.ShowAds(); //Shows Ads
AdMobUnityPlugin.HideAds(); //Hides Ads
AdMobUnityPlugin.SetPosition("middle","top");

Testing and Test Device ID's

If you want to be extra safe and avoid getting an Admob ban you should work in test mode.
First you will need your ‘Test Device ID’. The only way I know of to get this is to check the logcat in Eclipse while you device is plugged in 
(and running your game with Ads showing), search for a line that looks like: (search for 'Test' should find it)

To get test ads on this device, call adRequest.addTestDevice(“Your Test Device ID”);

Then call: (before calling StartAds())
AdMobUnityPlugin.SetTestMode("My Test Device ID in here");

Upgrading
The safest way is to delete the 'Android' and 'Demo_Admob' folders before importing the updated plugin package. 
Always backup your project and make a note of Ad Unit ID's etc that you will need to reinput into the AndroidManifest file.
If you get any compile errors after the update check that there is only one Google SDK jar in the Android folder (ie you didn't delete the 'Android' folder before updating)
	
Common Errors

You MUST deploy to an android device to see ads (test or real)
You must be properly signed up with some AdMob and added a live AdMob Ad unit ID 
How To Find Your AdMob AdMob Ad unit ID: (https://apps.admob.com)
1) Click "Monetize" Tab 2) click All apps, click on your app, you should see your Ad unit ID


New in v1.10
Interstitial Ads added


New in v1.03

try...catch statements added around AndroidJavaClass calls since Unity 4.2 now raises exceptions instead of warnings in editor


New in v1.02

Some renaming and moving of files to facilitate the Android Bundle


New in v1.01

New meta-data tag, "start_hidden", values can be "true" or "false",if "true", ad banner is initially hidden (defaults to "false"):

<meta-data android:name="start_hidden" android:value="true"/>

New Method, SetBanner(String bannertype), bannertypes are: BANNER,IAB_MRECT,IAB_BANNER,IAB_LEADERBOARD,SMART_BANNER. (defaults to "BANNER")

Is now compatible with other plugins that need to override the main activity, ie "com.platoevolved.admobunity.AdMobUnityActivity" doesn't need to be specified in the AndroidManifest any more. However at least one of your installed plugins need to do override the main activity for touches to work.

Version 6.1.0 of the Google Admob SDK now used



Disclaimer

DISCLAIMER / LIMITATION OF LIABILITY:

BUYER ACKNOWLEDGES THAT THE SOFTWARE MAY NOT BE

FREE FROM DEFECTS AND MAY NOT SATISFY ALL OF BUYER'S

NEEDS. IN NO EVENT WILL Morgan Page BE LIABLE FOR

DIRECT, INDIRECT, INCIDENTAL OR CONSEQUENTIAL DAMAGE

OR DAMAGES RESULTING FROM LOSS OF USE, OR LOSS OF

ANTICIPATED PROFITS RESULTING FROM ANY DEFECT IN THE

PROGRAM, EVEN IF IT HAS BEEN ADVISED OF THE

POSSIBILITY OF SUCH DAMAGE. SOME LAWS DO NOT ALLOW

THE EXCLUSION OR LIMITATION OF IMPLIED WARRANTIES OR

LIABILITIES FOR INCIDENTAL OR CONSEQUENTIAL DAMAGES,

SO THE ABOVE LIMITATIONS OR EXCLUSION MAY NOT APPLY.






