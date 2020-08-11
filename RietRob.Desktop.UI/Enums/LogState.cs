/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   LogState.cs
 *   Date			:   2020-08-11 11:14:04
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

namespace RietRob.Desktop.UI.Enums
{
    /// <summary>
    /// Represents the Log States. Like Debug or Warn or Error. 
    /// </summary>
    public enum LogState
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}