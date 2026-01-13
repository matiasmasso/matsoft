Public Class Frm_AeatMod347Item
    Private _Exercici As DTOExercici
    Private _Value As DTOAeatMod347Item

    Public Sub New(oItem As DTOAeatMod347Item, oExercici As DTOExercici)
        MyBase.New()
        Me.InitializeComponent()
        _Value = oItem
        _Exercici = oExercici
    End Sub

    Private Async Sub Frm_AeatMod347Item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim exs As New List(Of Exception)
        Me.Text = "Mod 347 " & _Value.Contact.Nom
        Dim oCompres = Await FEB.AeatMod347.CompresDetall(exs, _Exercici, _Value.Contact)
        Dim oVendes = Await FEB.AeatMod347.VendesDetall(exs, _Exercici, _Value.Contact)

        If exs.Count = 0 Then
            If oCompres.Count = 0 Then
                TabControl1.TabPages.Remove(TabPageCompres)
            Else
                Xl_AeatMod347CcasCompras.Load(oCompres)
            End If

            If oVendes.Count = 0 Then
                TabControl1.TabPages.Remove(TabPageVendes)
            Else
                Xl_AeatMod347CcasVendes.Load(oVendes)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

End Class