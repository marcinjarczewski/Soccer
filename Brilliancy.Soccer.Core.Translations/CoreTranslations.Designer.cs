﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Brilliancy.Soccer.Core.Translations {
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
    public class CoreTranslations {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CoreTranslations() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Brilliancy.Soccer.Core.Translations.CoreTranslations", typeof(CoreTranslations).Assembly);
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
        ///   Looks up a localized string similar to Klucz został już wcześniej użyty. Proszę wygenerować nowy..
        /// </summary>
        public static string Authentication_AlreadyConfirmed {
            get {
                return ResourceManager.GetString("Authentication_AlreadyConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email nie jest unikalny w skali aplikacji. Podaj nazwę użytkownika..
        /// </summary>
        public static string Authentication_EmailNotUnique {
            get {
                return ResourceManager.GetString("Authentication_EmailNotUnique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dane autoryzacyjne są niepoprawne. Skontaktuj się z administratorem..
        /// </summary>
        public static string Authentication_InvalidAuthData {
            get {
                return ResourceManager.GetString("Authentication_InvalidAuthData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nieprawidłowy klucz..
        /// </summary>
        public static string Authentication_InvalidKey {
            get {
                return ResourceManager.GetString("Authentication_InvalidKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Klucz wygasł. Proszę wygenerować nowy..
        /// </summary>
        public static string Authentication_KeyExpired {
            get {
                return ResourceManager.GetString("Authentication_KeyExpired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak adresu email..
        /// </summary>
        public static string Authentication_NoEmail {
            get {
                return ResourceManager.GetString("Authentication_NoEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nieprawidłowy link. Spróbuj wygenerować nowy klucz..
        /// </summary>
        public static string Authentication_NoKey {
            get {
                return ResourceManager.GetString("Authentication_NoKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak wskazanego zawodnika..
        /// </summary>
        public static string Authentication_NoPlayer {
            get {
                return ResourceManager.GetString("Authentication_NoPlayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Użytkownik nie istnieje..
        /// </summary>
        public static string Authentication_NoUser {
            get {
                return ResourceManager.GetString("Authentication_NoUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aby zostać administratorem, gracz musi mieć połączonego użytkownika..
        /// </summary>
        public static string Authentication_NoUserForInvite {
            get {
                return ResourceManager.GetString("Authentication_NoUserForInvite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Player already has an user..
        /// </summary>
        public static string Authentication_PlayerAlreadyHasUser {
            get {
                return ResourceManager.GetString("Authentication_PlayerAlreadyHasUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak wskazanego emaila..
        /// </summary>
        public static string Email_NoEmail {
            get {
                return ResourceManager.GetString("Email_NoEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak szablonu wiadomości email.
        /// </summary>
        public static string Email_NoTemplate {
            get {
                return ResourceManager.GetString("Email_NoTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił problem z emailem..
        /// </summary>
        public static string EmailSender_NoEmail {
            get {
                return ResourceManager.GetString("EmailSender_NoEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Adres email nie został podany..
        /// </summary>
        public static string EmailSender_NoEmailAddress {
            get {
                return ResourceManager.GetString("EmailSender_NoEmailAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił problem z konfiguracją usługi wysyłającą maile..
        /// </summary>
        public static string EmailSender_WrongConfig {
            get {
                return ResourceManager.GetString("EmailSender_WrongConfig", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login nie może być pusty..
        /// </summary>
        public static string Login_EmptyLogin {
            get {
                return ResourceManager.GetString("Login_EmptyLogin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wprowadzony login jest już zajęty..
        /// </summary>
        public static string Login_LoginInUse {
            get {
                return ResourceManager.GetString("Login_LoginInUse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił problem z autoryzacją..
        /// </summary>
        public static string Login_NoAuth {
            get {
                return ResourceManager.GetString("Login_NoAuth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Użytkownik nie istnieje lub jest nieaktywny..
        /// </summary>
        public static string Login_NoUser {
            get {
                return ResourceManager.GetString("Login_NoUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nieprawidłowy stan meczu.
        /// </summary>
        public static string Match_IncorrectState {
            get {
                return ResourceManager.GetString("Match_IncorrectState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak nazwy drużyny..
        /// </summary>
        public static string Match_NoTeamName {
            get {
                return ResourceManager.GetString("Match_NoTeamName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wskazany zawodnik nie bierze udziału w tym turnieju. .
        /// </summary>
        public static string Match_PlayerNotInTournament {
            get {
                return ResourceManager.GetString("Match_PlayerNotInTournament", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ten użytkownik jest już administratorem..
        /// </summary>
        public static string Tournament_AdminAlreadyAdded {
            get {
                return ResourceManager.GetString("Tournament_AdminAlreadyAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nieprawidłowy asystent przy bramce..
        /// </summary>
        public static string Tournament_InvalidAssist {
            get {
                return ResourceManager.GetString("Tournament_InvalidAssist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nieprawidłowy strzelec gola..
        /// </summary>
        public static string Tournament_InvalidScorer {
            get {
                return ResourceManager.GetString("Tournament_InvalidScorer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak miejsca rozegrania turnieju. .
        /// </summary>
        public static string Tournament_NoAddress {
            get {
                return ResourceManager.GetString("Tournament_NoAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie odnaleziono drużyny gości..
        /// </summary>
        public static string Tournament_NoAwayTeam {
            get {
                return ResourceManager.GetString("Tournament_NoAwayTeam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Błąd podczas przetwarzenia strzelonych bramek..
        /// </summary>
        public static string Tournament_NoGoals {
            get {
                return ResourceManager.GetString("Tournament_NoGoals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie odnaleziono drużyny gospodarzy..
        /// </summary>
        public static string Tournament_NoHomeTeam {
            get {
                return ResourceManager.GetString("Tournament_NoHomeTeam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie odnaleziono meczu..
        /// </summary>
        public static string Tournament_NoMatch {
            get {
                return ResourceManager.GetString("Tournament_NoMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak nazwy turnieju..
        /// </summary>
        public static string Tournament_NoName {
            get {
                return ResourceManager.GetString("Tournament_NoName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dane zawodnika nie zostały uzupełnione.
        /// </summary>
        public static string Tournament_NoPlayer {
            get {
                return ResourceManager.GetString("Tournament_NoPlayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak uprawnień do edycji turnieju..
        /// </summary>
        public static string Tournament_NoPrivileges {
            get {
                return ResourceManager.GetString("Tournament_NoPrivileges", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie odnaleziono turnieju..
        /// </summary>
        public static string Tournament_NoTournament {
            get {
                return ResourceManager.GetString("Tournament_NoTournament", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wskazano nieistniejącego użytkownika..
        /// </summary>
        public static string Tournament_NoUser {
            get {
                return ResourceManager.GetString("Tournament_NoUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Drużyna gospodarzy i gości nie może być taka sama..
        /// </summary>
        public static string Tournament_SameTeams {
            get {
                return ResourceManager.GetString("Tournament_SameTeams", resourceCulture);
            }
        }
    }
}
