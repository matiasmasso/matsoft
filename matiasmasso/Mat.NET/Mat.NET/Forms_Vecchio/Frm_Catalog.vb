Public Class Frm_Catalog

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oCatalog As ProductCatalog, Optional oSelectedProduct As DTOProduct0 = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        Xl_Catalog1.Load(oCatalog)
    End Sub

    Private Sub Xl_Catalog1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Catalog1.OnItemSelected
        RaiseEvent OnItemSelected(Me, e)
        Me.Close()
    End Sub
End Class
