
Public Class Frm_Cobrament_Reembolsos

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mPlan As PgcPlan = PgcPlan.FromToday
    Private mTransportista As Transportista
    Private mAllowEvents As Boolean
    Private mLastTabIdx As Integer
    Private mMail As DTOCorrespondencia

    Public WriteOnly Property Transportista() As Transportista
        Set(ByVal value As Transportista)
            mTransportista = value
            LoadTrps()
            PictureBoxLogo.Image = mTransportista.Img48
            Xl_AlbsReembols1.Transportista = mTransportista
            Xl_Cobrament1.PagadorNom = mTransportista.Abr
            mAllowEvents = True
        End Set
    End Property

    Private Sub LoadTrps()
        Dim SQL As String = "SELECT ALB.Pq_Trp AS ID, " _
        & "(CASE WHEN trp.abr IS NULL THEN '(' + CAST(ALB.Pq_Trp AS VARCHAR) + ')' ELSE TRP.ABR END) AS NOM " _
        & "FROM ALB LEFT OUTER JOIN " _
        & "TRP ON ALB.Emp = TRP.emp AND ALB.Pq_Trp = TRP.Id " _
        & "WHERE ALB.CashCod =" & DTO.DTOCustomer.CashCodes.Reembols & " AND " _
        & "ALB.cobro IS NULL AND " _
        & "ALB.Emp =" & BLLApp.Emp.Id & " And " _
        & "Alb.Pts > 0 and " _
        & "ALB.Pq_Trp<>0 " _
        & "GROUP BY ALB.Pq_Trp, trp.abr " _
        & "ORDER BY COUNT(ALB.ALB) DESC"
        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        With ComboBoxTrp
            .ValueMember = "ID"
            .DisplayMember = "NOM"
            .DataSource = oDs.Tables(0)
            If oDs.Tables(0).Rows.Count > 0 Then
                '.SelectedIndex = 0
            End If
            .SelectedValue = mTransportista.Id
        End With
    End Sub

    Private Sub Save()
        'Protegeix de doble grabat per botó i selecció de Tab
        Static BlDone As Boolean
        If BlDone Then Exit Sub
        BlDone = True

        Dim oplan As PgcPlan = PgcPlan.FromYear(DateTimePicker1.Value.Year)

        If Xl_Cobrament1.CodiFpg <> Xl_Cobrament.Fpgs.Cash Then
            SaveMail()
        End If

        Dim oAlbs As Albs = Xl_AlbsReembols1.Albs
        Dim oAlb As Alb
        For Each oAlb In oAlbs
            SaveCcaItm(oAlb)
        Next

        Select Case Xl_Cobrament1.CodiFpg
            Case Xl_Cobrament.Fpgs.Cash
                Me.Close()
            Case Xl_Cobrament.Fpgs.Transfer
                SaveCca()
            Case Xl_Cobrament.Fpgs.Xec, Xl_Cobrament.Fpgs.Pagare
                SaveXec()
        End Select
    End Sub

    Private Sub SaveMail()
        mMail = New DTOCorrespondencia()
        With mMail
            .UserLastEdited = BLLSession.Current.User
            .Fch = DateTimePicker1.Value
            .Cod = DTO.DTOCorrespondencia.Cods.Rebut
            .Contacts.Add(New DTOContact(mTransportista.Guid))
            .Subject = "RELACIÓ DE REEMBOLSOS " & Xl_AlbsReembols1.Amt.CurFormatted
            Dim exs As New List(Of Exception)
            If BLLCorrespondencia.Update(mMail, exs) Then
                TextBoxEnd.Text = "correspondencia num. " & mMail.Id
            Else
                MsgBox("error al desar el document a correspondencia" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        End With
    End Sub

    Private Sub SaveCca()
        Dim oCtaHab As DTOPgcCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.DeutorsVaris)
        Dim oCtaDeb As DTOPgcCta = Xl_Cobrament1.Cta
        Dim oAmt As DTOAmt = Xl_AlbsReembols1.Amt


        Dim oCca As DTOCca = BLLCca.Factory(DateTimePicker1.Value, BLLSession.Current.User, DTOCca.CcdEnum.Reemborsament)
        oCca.Concept = "n/ref." & mMail.Id & " " & Xl_Cobrament1.Concepte

        BLLCca.AddDebit(oCca, oAmt, oCtaDeb, Xl_Cobrament1.SubCta)
        BLLCca.AddCredit(oCca, oAmt, oCtaHab, mTransportista.ToDTO)

        Dim exs As New List(Of Exception)
        If BLLCca.Update(oCca, exs) Then
            TextBoxEnd.Text = oCca.Concept & " assentament " & oCca.Id
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub SaveCcaItm(ByVal oalb As Alb)
        Dim exs As New List(Of Exception)
        Dim oCtaDeb As PgcCta
        Dim oContactDeb As Contact
        Dim oCtaHab As PgcCta = mPlan.Cta(DTOPgcPlan.ctas.clients)


        Select Case Xl_Cobrament1.CodiFpg
            Case Xl_Cobrament.Fpgs.Cash
                oCtaDeb = mPlan.Cta(DTOPgcPlan.ctas.caixa)
                oContactDeb = Nothing
            Case Else
                oCtaDeb = mPlan.Cta(DTOPgcPlan.ctas.DeutorsVaris)
                oContactDeb = mTransportista
        End Select


        Dim oAmt As DTOAmt = oalb.Reembolso

        Dim oCca As New cca(BLL.BLLApp.emp)
        With oCca
            .Ccd = DTOCca.CcdEnum.Reemborsament
            .fch = DateTimePicker1.Value
            .Txt = mTransportista.Abr & "-reemb. alb." & oalb.Id & " a " & oalb.Nom
            .ccbs.Add(New Ccb(oCca, oCtaHab, oalb.Client.CcxOrMe, oAmt, DTOCcb.DhEnum.Haber))
            .ccbs.Add(New Ccb(oCca, oCtaDeb, oContactDeb, oAmt, DTOCcb.DhEnum.Debe))

            If Not .Update(exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With

        oalb.Cobra(oCca.fch, exs)
    End Sub

    Private Sub SaveXec()
        Dim oCta As DTOPgcCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.DeutorsVaris)
        Dim oAmt As DTOAmt = Xl_AlbsReembols1.Amt

        Dim oPnd As New DTOPnd
        With oPnd
            .Contact = mTransportista.ToDTO
            .Amt = oAmt
            .Cod = Pnd.Codis.Deutor
            .Cta = oCta
            .Fch = Today
            .Vto = Xl_Cobrament1.XecVto
            .Cfp = DTOPaymentTerms.CodsFormaDePago.ReposicioFons
            .Fpg = "xec reembolsos, pendent de ingressar"
        End With

        Dim oPnds As New List(Of DTOPnd)
        oPnds.Add(oPnd)

        Dim oXec As New DTOXec
        With oXec
            .Lliurador = mTransportista.ToDTO
            .Iban = Xl_Cobrament1.XecIBAN
            .XecNum = Xl_Cobrament1.XecNum
            .Pnds = oPnds
        End With

        If Xl_Cobrament1.CodiFpg = Xl_Cobrament.Fpgs.Pagare Then
            oXec.Vto = Xl_Cobrament1.XecVto
        End If

        Dim exs as New List(Of exception)
        If BLLXec.Update(oXec, exs) Then
            TextBoxEnd.Text = "xec registrat i pendent de ingressar"
        Else
            UIHelper.WarnError( exs, "error al desar el xec")
        End If
    End Sub

    Private Sub EnableButtons()
        Dim BlEnableNext As Boolean = True
        Dim BlEnablePrevious As Boolean = True
        Dim BlEnableEnd As Boolean = False
        Dim sCaptionEnd As String = "FI >>"

        mLastTabIdx = TabControl1.SelectedIndex
        Select Case TabControl1.SelectedTab.Text
            Case TabPagePndSel.Text
                BlEnablePrevious = False
                BlEnableNext = (Xl_AlbsReembols1.Albs.Count > 0)
            Case TabPageFpg.Text
                BlEnableNext = False
                BlEnableEnd = Xl_Cobrament1.CheckComplete
            Case TabPageEnd.Text
                BlEnablePrevious = False
                BlEnableNext = False
                BlEnableEnd = True
                sCaptionEnd = "SORTIDA"
        End Select

        ButtonPrevious.Enabled = BlEnablePrevious
        ButtonNext.Enabled = BlEnableNext
        ButtonEnd.Enabled = BlEnableEnd
        ButtonEnd.Text = sCaptionEnd
    End Sub

    Private Sub ButtonPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrevious.Click
        Dim Idx As Integer = TabControl1.SelectedIndex
        TabControl1.SelectedTab = TabControl1.TabPages(Idx - 1)
        EnableButtons()
        'Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext.Click
        Dim Idx As Integer = TabControl1.SelectedIndex
        TabControl1.SelectedTab = TabControl1.TabPages(Idx + 1)
        EnableButtons()
        'Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        Select Case TabControl1.SelectedTab.Text
            Case TabPageEnd.Text
                'si ja estaba a l'ultim tab, surt.
                Me.Close()
            Case Else
                TabControl1.SelectedTab() = TabPageEnd
                Save()
        End Select
        'Wizard_AfterTabSelect()
    End Sub



    Private Sub Xl_Cobrament1_AfterUpdate() Handles Xl_Cobrament1.AfterUpdate
        EnableButtons()
    End Sub



    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim AllowEvent As Boolean = False

        Select Case TabControl1.SelectedIndex
            Case TabControl1.TabPages.Count - 1
                AllowEvent = ButtonEnd.Enabled
            Case Is > mLastTabIdx
                AllowEvent = ButtonNext.Enabled
            Case Is < mLastTabIdx
                AllowEvent = ButtonPrevious.Enabled
        End Select

        If AllowEvent Then
            If TabControl1.SelectedIndex = TabControl1.TabPages.Count - 1 Then
                Save()
            End If
            EnableButtons()
        Else
            TabControl1.SelectedTab = TabControl1.TabPages(mLastTabIdx)
        End If
    End Sub


    Private Function CurrentTransportista() As Transportista
        Dim LngId As Long = ComboBoxTrp.SelectedValue
        Dim oTrp As Transportista = MaxiSrvr.Transportista.FromNum(mEmp, LngId)
        Return oTrp
    End Function

    Private Sub ComboBoxTrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxTrp.SelectedIndexChanged
        If mAllowEvents Then
            mTransportista = CurrentTransportista()
            PictureBoxLogo.Image = mTransportista.Img48
            Xl_AlbsReembols1.Transportista = mTransportista
            Xl_Cobrament1.PagadorNom = mTransportista.Abr
        End If
    End Sub

    Private Sub Xl_AlbsReembols1_AfterUpdate() Handles Xl_AlbsReembols1.AfterUpdate
        EnableButtons()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        mPlan = PgcPlan.FromYear(DateTimePicker1.Value.Year)
    End Sub
End Class