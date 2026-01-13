

Public Class Frm_Avisame
    Private mEmail As Email
    Private mArt As Art

    Private Enum Cols
        Id
        ArtNom
        FchQuery
        FchWarning
        Obs
    End Enum

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Email() As Email
        Set(ByVal value As Email)
            mEmail = value
            Refresca()
        End Set
    End Property

    Private Sub Refresca()
        TextBoxEmail.Text = mEmail.Adr
        Dim SQL As String = "SELECT AVISAME.ID,ART.MYD,AVISAME.FCHQUERY,AVISAME.FCHWARNING,AVISAME.OBS " _
        & "FROM ART INNER JOIN " _
        & "Stp ON Stp.Guid = Art.Category INNER JOIN " _
        & "AVISAME ON ART.emp = AVISAME.emp AND ART.art = AVISAME.art INNER JOIN " _
        & "Tpa ON Tpa.Guid = Stp.Brand " _
        & "WHERE AVISAME.EmailGuid=@EmailGuid "

        If CheckBoxHideOlds.Checked Then
            SQL = SQL & " AND AVISAME.FCHWARNING IS NULL "
        End If
        SQL = SQL & "ORDER BY TPA.ORD, STP.ORD, ART.ORD"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@EmailGuid", mEmail.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "producte"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.FchQuery)
                .HeaderText = "solicitud"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.FchWarning)
                If CheckBoxHideOlds.Checked Then
                    .Visible = False
                Else
                    .Visible = True
                    .HeaderText = "avisat"
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "s/referencia"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

    End Sub

    Private Sub TextBoxArt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxArt.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim sKey As String = TextBoxArt.Text
            If Not mArt Is Nothing Then
                If sKey = mArt.Nom_ESP Then
                    root.ShowArt(mArt)
                    Exit Sub
                End If
            End If

            ButtonAdd.Enabled = False

            If sKey = "" Then
                mArt = Nothing
            Else
                Dim oSku As DTOProductSku = Finder.FindSku(sKey, BLL.BLLApp.Mgz)
                If oSku Is Nothing Then
                    mArt = Nothing
                Else
                    mArt = New Art(oSku.Guid)
                End If
            End If
        End If
    End Sub

    Private Sub ArtFound(ByVal sender As Object, ByVal e As System.EventArgs)
        mArt = CType(sender, Art)
        If mArt IsNot Nothing Then
            TextBoxArt.Text = mArt.Nom_ESP
            PictureBoxArt.Image = mArt.Image
            ButtonAdd.Enabled = True
        End If
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Dim oAvisame As New Avisame(Avisame.Srcs.Oficina, mEmail, mArt, TextBoxObs.Text)
        oAvisame.Update()
        RaiseEvent AfterUpdate(sender, e)
        mArt = Nothing
        TextBoxArt.Text = ""
        ButtonAdd.Enabled = False
        Refresca()
    End Sub
End Class