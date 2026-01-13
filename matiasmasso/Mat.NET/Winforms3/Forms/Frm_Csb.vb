Public Class Frm_Csb
    Private _Csb As DTOCsb
    Private _LogsLoaded As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Logs
    End Enum

    Public Sub New(value As DTOCsb)
        MyBase.New()
        Me.InitializeComponent()
        _Csb = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Csb.Load(_Csb, exs) Then

            With _Csb
                Dim oCsa As DTOCsa = _Csb.Csa
                If oCsa IsNot Nothing Then
                    TextBoxCsa.Text = "document " & _Csb.FormattedId() & " presentat el " & oCsa.Fch.ToShortDateString & " a " & oCsa.Banc.Abr
                End If
                TextBoxCliNom.Text = _Csb.Contact.Nom
                TextBoxVto.Text = _Csb.Vto.ToShortDateString

                TextBoxEur.Text = DTOAmt.CurFormatted(_Csb.Amt)
                TextBoxTxt.Text = _Csb.Txt
                Await Xl_Iban1.Load(_Csb.Iban)

                TextBoxMandato.Text = _Csb.Iban.Guid.ToString

                TextBoxResult.Text = _Csb.Result.ToString
                Select Case _Csb.Result
                    Case DTOCsb.Results.Pendent
                    Case DTOCsb.Results.Vençut
                        TextBoxResult.BackColor = Color.LightBlue
                    Case DTOCsb.Results.Reclamat
                        TextBoxResult.BackColor = Color.Beige
                    Case DTOCsb.Results.Impagat
                        TextBoxResult.BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                End Select

                If _Csb.ResultCca IsNot Nothing Then
                    TextBoxResultCca.Text = DTOCca.FullNom(_Csb.ResultCca)
                End If

                Dim oMenu_Csb As New Menu_Csb2(_Csb)
                ArxiuToolStripMenuItem.DropDownItems.AddRange(oMenu_Csb.Range)
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub ReclamarToolStripButton_Click(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("reclamem aquest efecte?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim oCca = Await FEB.Csb.Reclama(exs, Current.Session.User, _Csb)
            If exs.Count = 0 Then
                Dim oMail = DTOCorrespondencia.Factory(Current.Session.User, _Csb.Csa.Banc, DTOCorrespondencia.Cods.Enviat)
                With oMail
                    .Subject = "RECLAMACIO EFECTE " & _Csb.FormattedId()
                    FEB.Csb.Load(_Csb, exs)
                    FEB.Banc.Load(_Csb.Csa.Banc, exs)
                    FEB.Contact.Load(_Csb.Csa.Banc, exs)
                    Dim oDoc As New LegacyHelper.PdfBancReclamacioEfecte(_Csb, oMail)
                    .docFile = LegacyHelper.DocfileHelper.Factory(exs, oDoc.Stream(exs), MimeCods.Pdf)
                    If Await FEB.DocFile.Upload(.DocFile, exs) Then
                        If Await FEB.Correspondencia.Upload(oMail, exs) Then
                            If Await UIHelper.ShowStreamAsync(exs, oMail.DocFile) Then
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        Else
                            UIHelper.WarnError(exs, "error al desar el document a correspondencia")
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End With
            Else
                UIHelper.WarnError(exs, "error al reclamar l'efecte")
            End If
        Else
            UIHelper.WarnError("Operació cancelada per l'usuari")
        End If
    End Sub

    Private Sub TextBoxResultCca_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxResultCca.DoubleClick
        Dim oCca As DTOCca = _Csb.ResultCca
        If oCca IsNot Nothing Then
            Dim oFrm As New Frm_Cca(oCca)
            oFrm.Show()
        End If
    End Sub

    Private Sub TextBoxMandato_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxMandato.DoubleClick
        Dim oFrm As New Frm_Iban(_Csb.Iban)
        oFrm.Show()
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Logs
                If Not _LogsLoaded Then
                    Dim exs As New List(Of Exception)
                    Dim oLogs = Await FEB.MailingLogs.All(exs, _Csb)
                    If exs.Count = 0 Then
                        Xl_MailingLogs1.Load(oLogs)
                        _LogsLoaded = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub
End Class


