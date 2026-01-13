

Public Class Frm_RepProducts
    Private _Rep As DTORep
    Private _AllowEvents As Boolean

    Public Sub New(oRep As DTORep)
        MyBase.New()
        Me.InitializeComponent()
        _Rep = oRep
    End Sub

    Private Async Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Rep.Load(exs, _Rep) Then
            Me.Text = _Rep.NickName & " - Cartera de Productes"
            Dim items = Await FEB.RepProducts.All(exs, Current.Session.Emp, _Rep, True)
            If exs.Count = 0 Then
                Xl_RepProducts1.Load(items, Xl_RepProducts.Modes.ByRep)
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class