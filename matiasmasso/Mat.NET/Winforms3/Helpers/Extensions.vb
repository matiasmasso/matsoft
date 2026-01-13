Imports System.Reflection

Public Module Extensions


    <System.Runtime.CompilerServices.Extension()>
    Public Sub DoubleBuffered(ByVal control As Xl_CheckedTreeView, enabled As Boolean)
        Dim prop = control.[GetType]().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        prop.SetValue(control, enabled, Nothing)
    End Sub
End Module