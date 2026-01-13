Public Class Frm_PrvCliNums
    Private _Proveidor As DTOProveidor

    Public Sub New(oProveidor As DTOProveidor)
        MyBase.New()
        Me.InitializeComponent()
        _Proveidor = oProveidor
    End Sub

    Private Async Sub Frm_PrvCliNums_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Me.Text & " " & _Proveidor.FullNom
        Dim exs As New List(Of Exception)
        Dim oPrvclinumns = Await FEB2.PrvCliNums.All(_Proveidor, exs)
        If exs.Count = 0 Then
            Xl_PrvCliNums1.Load(oPrvclinumns)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class