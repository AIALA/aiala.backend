﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace aiala.Backend.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MailMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MailMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("aiala.Backend.Resources.MailMessages", typeof(MailMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ended.
        /// </summary>
        public static string EmergencyMailEndEventName {
            get {
                return ResourceManager.GetString("EmergencyMailEndEventName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Emergency ended.
        /// </summary>
        public static string EmergencyMailEndProtocolEntry {
            get {
                return ResourceManager.GetString("EmergencyMailEndProtocolEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The app tracked the location &lt;a href=&quot;{link}&quot;&gt;somewhere close to here&lt;/a&gt;..
        /// </summary>
        public static string EmergencyMailLinkText {
            get {
                return ResourceManager.GetString("EmergencyMailLinkText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hi {recipientName}&lt;br/&gt;
        ///&lt;br/&gt;
        ///At {time} on {date}, {userName} {eventName} an emergency. {linkText}&lt;br/&gt;
        ///&lt;br/&gt;
        ///{taskBlock}
        ///{protocolBlock}
        ///Regards,&lt;br/&gt;
        ///The AIALA Team.
        /// </summary>
        public static string EmergencyMailMessage {
            get {
                return ResourceManager.GetString("EmergencyMailMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mood: {moodName}.
        /// </summary>
        public static string EmergencyMailMoodProtocolEntry {
            get {
                return ResourceManager.GetString("EmergencyMailMoodProtocolEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to started.
        /// </summary>
        public static string EmergencyMailStartEventName {
            get {
                return ResourceManager.GetString("EmergencyMailStartEventName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Emergency started.
        /// </summary>
        public static string EmergencyMailStartProtocolEntry {
            get {
                return ResourceManager.GetString("EmergencyMailStartProtocolEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Emergency at {time} on {date}.
        /// </summary>
        public static string EmergencyMailSubject {
            get {
                return ResourceManager.GetString("EmergencyMailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to updated.
        /// </summary>
        public static string EmergencyMailUpdateEventName {
            get {
                return ResourceManager.GetString("EmergencyMailUpdateEventName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bad.
        /// </summary>
        public static string EmergencyMoodBad {
            get {
                return ResourceManager.GetString("EmergencyMoodBad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Better.
        /// </summary>
        public static string EmergencyMoodBetter {
            get {
                return ResourceManager.GetString("EmergencyMoodBetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Improving.
        /// </summary>
        public static string EmergencyMoodImproving {
            get {
                return ResourceManager.GetString("EmergencyMoodImproving", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Protocol of the emergency:&lt;br/&gt;
        ///{protocol}
        ///&lt;br/&gt;.
        /// </summary>
        public static string EmergencyProtocolBlock {
            get {
                return ResourceManager.GetString("EmergencyProtocolBlock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  Active task: {taskName}&lt;br/&gt;
        ///&lt;br/&gt;.
        /// </summary>
        public static string EmergencyTaskBlock {
            get {
                return ResourceManager.GetString("EmergencyTaskBlock", resourceCulture);
            }
        }
    }
}
