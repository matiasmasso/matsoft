

Public Class Frm_Txts
    Private mTxt As Txt = Nothing

    Private mAllowEvents As Boolean = False

    Private Sub Frm_Txts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadIds()
        mAllowEvents = True
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        If ComboBoxIds.SelectedIndex >= 0 Then
            Dim Id As Txt.Ids = CType(ComboBoxIds.SelectedItem, maxisrvr.MatListItem).Value
            mTxt = New Txt(Id)
            TextBoxEsp.Text = Txt.ExpandHtmlTables(mTxt.Esp)
            TextBoxCat.Text = Txt.ExpandHtmlTables(mTxt.Cat)
            TextBoxEng.Text = Txt.ExpandHtmlTables(mTxt.Eng)
        End If
    End Sub

    Private Sub LoadIds()
        For Each v As Integer In [Enum].GetValues(GetType(Txt.Ids))
            If CType(v, Txt.Ids) <> Txt.Ids.NotSet Then
                Dim oTxt As New Txt(v)
                Dim oItem As New maxisrvr.MatListItem(v, oTxt.Tit)
                ComboBoxIds.Items.Add(oItem)
            End If
        Next
    End Sub

    Private Sub ComboBoxIds_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxIds.SelectedIndexChanged
        If mAllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mTxt
            .Esp = Txt.FlattenHtmlTables(TextBoxEsp.Text)
            .Cat = Txt.FlattenHtmlTables(TextBoxCat.Text)
            .Eng = Txt.FlattenHtmlTables(TextBoxEng.Text)
            .Update()
        End With
        Me.Close()
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxEsp.TextChanged, _
        TextBoxCat.TextChanged, _
        TextBoxEng.TextChanged

        ButtonOk.Enabled = True
    End Sub
End Class