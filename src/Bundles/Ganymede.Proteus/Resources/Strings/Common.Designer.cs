﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheXDS.Ganymede.Resources.Strings; 
using System;


/// <summary>
///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
/// </summary>
// StronglyTypedResourceBuilder generó automáticamente esta clase
// a través de una herramienta como ResGen o Visual Studio.
// Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
// con la opción /str o recompile su proyecto de VS.
[global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
internal class Common {
    
    private static global::System.Resources.ResourceManager resourceMan;
    
    private static global::System.Globalization.CultureInfo resourceCulture;
    
    [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    internal Common() {
    }
    
    /// <summary>
    ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    internal static global::System.Resources.ResourceManager ResourceManager {
        get {
            if (object.ReferenceEquals(resourceMan, null)) {
                global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TheXDS.Ganymede.Resources.Strings.Common", typeof(Common).Assembly);
                resourceMan = temp;
            }
            return resourceMan;
        }
    }
    
    /// <summary>
    ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
    ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    internal static global::System.Globalization.CultureInfo Culture {
        get {
            return resourceCulture;
        }
        set {
            resourceCulture = value;
        }
    }
    
    /// <summary>
    ///   Busca una cadena traducida similar a Are you sure you want to perform this operation?.
    /// </summary>
    internal static string AreYouSure {
        get {
            return ResourceManager.GetString("AreYouSure", resourceCulture);
        }
    }
    
    /// <summary>
    ///   Busca una cadena traducida similar a Edit {0} &quot;{1}&quot;.
    /// </summary>
    internal static string EditModel {
        get {
            return ResourceManager.GetString("EditModel", resourceCulture);
        }
    }
    
    /// <summary>
    ///   Busca una cadena traducida similar a New {0}.
    /// </summary>
    internal static string NewModel {
        get {
            return ResourceManager.GetString("NewModel", resourceCulture);
        }
    }
    
    /// <summary>
    ///   Busca una cadena traducida similar a No.
    /// </summary>
    internal static string No {
        get {
            return ResourceManager.GetString("No", resourceCulture);
        }
    }
    
    /// <summary>
    ///   Busca una cadena traducida similar a Saving data....
    /// </summary>
    internal static string Saving {
        get {
            return ResourceManager.GetString("Saving", resourceCulture);
        }
    }
    
    /// <summary>
    ///   Busca una cadena traducida similar a Are you sure you want to cancel? All changes made will be lost..
    /// </summary>
    internal static string WantToCancelChanges {
        get {
            return ResourceManager.GetString("WantToCancelChanges", resourceCulture);
        }
    }
    
    /// <summary>
    ///   Busca una cadena traducida similar a Yes.
    /// </summary>
    internal static string Yes {
        get {
            return ResourceManager.GetString("Yes", resourceCulture);
        }
    }
}
