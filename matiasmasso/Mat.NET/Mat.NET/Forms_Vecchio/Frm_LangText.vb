

Public Class Frm_LangText
    Private mLangResource As LangResource
    Private mLangText As LangText
    Private mAllowEvents As Boolean
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property LangResource() As LangResource
        Set(ByVal value As LangResource)
            mLangResource = value
        End Set
    End Property

    Public Property LangText() As LangText
        Get
            Return mLangText
        End Get
        Set(ByVal value As LangText)
            mLangText = value
            LoadLangs()
            TextBoxText.Text = mLangText.Text
            mAllowEvents = True
        End Set
    End Property

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        mLangText = new LangText(CurrentLang, TextBoxText.Text)
        RaiseEvent AfterUpdate(mLangText, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub TextBoxText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxText.TextChanged
        If mAllowEvents Then
            mLangText.Text = TextBoxText.Text
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub LoadLangs()
        Dim SQL As String = ""
        Dim oTb As DataTable = Nothing
        If mLangText.Lang.Id = DTOLang.Ids.NotSet Then
            SQL = "SELECT Abr FROM LANG WHERE Id>1 ORDER BY Id"
            Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
            oTb = oDs.Tables(0)
            For i As Integer = oTb.Rows.Count - 1 To 0 Step -1
                For Each oLangText As LangText In mLangResource.LangTexts
                    If oTb.Rows(i)("Abr") = oLangText.Lang.Tag Then
                        oTb.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next
            Next
        Else
            SQL = "SELECT Abr FROM LANG WHERE ID=@LANGID"
            Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@LANGID", mLangText.Lang.Id)
            oTb = oDs.Tables(0)
        End If

        With ComboBoxLang
            .DataSource = oTb
            .DisplayMember = "Abr"
            .ValueMember = "Abr"
        End With
        SetFlag()
    End Sub

    Private Function CurrentLang() As DTOLang
        Dim oLang As New DTOLang(ComboBoxLang.SelectedValue.ToString)
        Return oLang
    End Function

    Private Sub SetFlag()
        'PictureBoxFlag.Image = CurrentLang.Flag
    End Sub

    Private Sub ComboBoxLang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxLang.SelectedIndexChanged
        If mAllowEvents Then
            SetFlag()
        End If
    End Sub
End Class