﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace documentdbTest.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://kimanju.documents.azure.com:443/")]
        public string DocumentDbURI {
            get {
                return ((string)(this["DocumentDbURI"]));
            }
            set {
                this["DocumentDbURI"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("QHAl9z5TKEcgwHrDkyg9lIjnbZvnANvNJgFvzF4xVBvcoJ6Bxj9OHNkMloq8fLnmqmlM01w55wdcq4KGV" +
            "XgPHA==")]
        public string DocumentDbPrimaryKey {
            get {
                return ((string)(this["DocumentDbPrimaryKey"]));
            }
            set {
                this["DocumentDbPrimaryKey"] = value;
            }
        }
    }
}
