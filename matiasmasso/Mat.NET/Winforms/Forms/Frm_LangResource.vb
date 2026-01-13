Public Class Frm_LangResource
    Private _LangText As DTOLangText

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oLangText As DTOLangText)
        InitializeComponent()
        _LangText = oLangText
    End Sub

    Public Sub New(oBaseGuid As DTOBaseGuid, oSrc As DTOLangText.Srcs)
        InitializeComponent()
        _LangText = New DTOLangText(oBaseGuid.Guid, oSrc)
    End Sub

    Private Sub Frm_LangResource_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.LangText.Load(exs, _LangText) Then
            Xl_LangsText1.Load(_LangText)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_LangsText1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LangsText1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        _LangText = Xl_LangsText1.Value
        If Await FEB2.LangText.Update(exs, _LangText) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_LangText))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class