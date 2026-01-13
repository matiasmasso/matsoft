
Public Class Frm_Txts
    Private _Txt As DTOTxt

    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Async Sub Frm_Txts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadIds()
        _AllowEvents = True
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Function refresca() As Task
        If ComboBoxIds.SelectedIndex >= 0 Then
            Dim Id As DTOTxt.Ids = DirectCast(ComboBoxIds.SelectedItem, MatListItem).Value
            Dim exs As New List(Of Exception)
            _Txt = Await FEB.Txt.Find(Id, exs)
            If exs.Count = 0 Then
                If _Txt Is Nothing Then
                    _Txt = New DTOTxt(Id)
                End If
                Xl_LangsText1.Load(_Txt.LangText)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Sub LoadIds()
        For Each v As Integer In [Enum].GetValues(GetType(DTOTxt.Ids))
            If DirectCast(v, DTOTxt.Ids) <> DTOTxt.Ids.NotSet Then
                Dim oTxt As New DTOTxt(v)
                Dim oItem As New MatListItem(v, DTOTxt.Tit(oTxt))
                ComboBoxIds.Items.Add(oItem)
            End If
        Next
    End Sub

    Private Async Sub ComboBoxIds_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxIds.SelectedIndexChanged
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

        _Txt.LangText = Xl_LangsText1.Value
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.Txt.Update(_Txt, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Txt))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_LangsText1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LangsText1.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class