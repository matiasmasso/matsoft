

Public Class Frm_Csb
    Private mCsb As Csb

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Csb() As Csb
        Set(ByVal value As Csb)
            If value IsNot Nothing Then
                mCsb = value
                Me.Text = "EFECTE " & mCsb.Document
                refresca()
            End If
        End Set
    End Property

    Private Sub Refresca()
        Dim oCsa As Csa = mCsb.Csa
        PictureBoxBancLogo.Image = oCsa.Banc.Img48
        TextBoxCsa.Text = "remesa presentada el " & oCsa.fch.ToShortDateString & " a " & oCsa.Banc.Abr
        TextBoxCliNom.Text = mCsb.Client.Clx
        TextBoxVto.Text = mCsb.Vto.ToShortDateString

        If mCsb.VtoCca IsNot Nothing Then
            Dim oCca As Cca = mCsb.VtoCca
            TextBoxCcaVto.Text = "assentament " & oCca.yea.ToString & "." & oCca.Id
            ButtonCcaVto.Enabled = True
        End If

        TextBoxEur.Text = mCsb.Amt.CurFormatted
        TextBoxTxt.Text = mCsb.txt
        PictureBoxIBAN.Image = BLL.BLLIban.Img(mCsb.Iban, BLLSession.Current.Lang)


        ReclamarToolStripButton.Enabled = (mCsb.VtoCca Is Nothing)

        ToolStripButtonImpagat.Enabled = mCsb.Impagat

        If mCsb.Impagat Then
            TextBox1.Text = "IMPAGAT"
            TextBox1.BackColor = MaxiSrvr.COLOR_NOTOK
        ElseIf mCsb.Reclamat Then
            TextBox1.Text = "RECLAMAT"
            TextBox1.BackColor = Color.Beige
        ElseIf mCsb.VtoCca Is Nothing Then
            TextBox1.Text = "EN CIRCULACIO"
        Else
            TextBox1.Text = "VENÇUT"
        End If
    End Sub

    Private Sub ContextMenuStripCsa_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuStripCsa.Opened
        Static BlDone As Boolean
        If BlDone Then Exit Sub
        BlDone = True
        ContextMenuStripCsa.Items.AddRange(New Menu_Csa(mCsb.Csa).Range)
    End Sub

    Private Sub ContextMenuStripClient_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuStripClient.Opened
        Static BlDone As Boolean
        If BlDone Then Exit Sub
        BlDone = True
        ContextMenuStripClient.Items.AddRange(New Menu_Contact(mCsb.Client).Range)
    End Sub

    Private Sub RebutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RebutToolStripButton.Click
        root.ShowRebut(mCsb.Rebut)
    End Sub

    Private Sub ReclamarToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReclamarToolStripButton.Click
        Dim rc As MsgBoxResult = MsgBox("reclamem aquest efecte?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mCsb.Reclama( exs) Then
                Dim oMail As New Mail(mCsb.Csa.emp, Today)
                With oMail
                    .Contacts.Add(mCsb.Csa.Banc)
                    .Cod = DTO.DTOCorrespondencia.Cods.Enviat
                    .Subject = "RECLAMACIO EFECTE " & mCsb.Document
                    Dim oDoc As New PdfBancReclamacioEfecte(mCsb, oMail)

                    Dim oDocFile As DTODocFile = Nothing
                    If BLL_DocFile.LoadFromStream(oDoc.Stream, oDocFile, exs) Then
                        .DocFile = oDocFile
                        .DocFile.PendingOp = DTODocFile.PendingOps.Update
                    End If
                End With

                If oMail.Update( exs) Then
                    UIHelper.ShowStream(oMail.DocFile)
                    Me.Close()
                Else
                    UIHelper.WarnError( exs, "error al desar el document a correspondencia")
                End If

            Else
                UIHelper.WarnError( exs, "error al reclamar l'efecte")
            End If
        Else
            UIHelper.WarnError("Operació cancelada per l'usuari")
        End If
    End Sub


    Private Sub ToolStripButtonImpagat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonImpagat.Click
        Dim oImpagat As New Impagat(mCsb)
        Dim oFrm As New Frm_Impagat
        oFrm.Impagat = oImpagat.ToDTO
        oFrm.Show()
    End Sub

    Private Sub ButtonCcaVto_Click(sender As Object, e As EventArgs) Handles ButtonCcaVto.Click
        Dim DtFch As DateTime = mCsb.Vto
        Dim oExercici As New DTOExercici(BLL.BLLApp.Emp, DtFch.Year)

        Dim oCta As DTOPgcCta = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.BancsEfectesDescomptats, oExercici)
        Dim oFrm As New Frm_Extracte(mCsb.Client.ToNewContact, oCta, oExercici, BLL.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onNewCcaSelected
        oFrm.Show()
    End Sub

    Private Sub onNewCcaSelected(sender As Object, e As MatEventArgs)
        Dim oCca As Cca = e.Argument
        If oCca IsNot Nothing Then
            mCsb.VtoCca = oCca
            TextBoxCcaVto.Text = "assentament " & oCca.yea.ToString & "." & oCca.Id
            ButtonCcaVto.Enabled = True
            Dim exs as New List(Of exception)
            If Not mCsb.Update( exs) Then
                UIHelper.WarnError( exs, "error al canviar l'assentament de venciment")
            End If
        End If
    End Sub

    Private Sub PictureBoxVtoCcaBrowse_Click(sender As Object, e As EventArgs) Handles PictureBoxVtoCcaBrowse.Click
        If mCsb.VtoCca IsNot Nothing Then
            Dim oCca As Cca = mCsb.VtoCca
            Dim oFrm As New Frm_Cca(oCca)
            oFrm.Show()
        End If
    End Sub



    Private Sub TextBoxCliNom_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxCliNom.DoubleClick
        Dim oFrm As New Frm_Contact(mCsb.Client)
        oFrm.Show()
    End Sub
End Class