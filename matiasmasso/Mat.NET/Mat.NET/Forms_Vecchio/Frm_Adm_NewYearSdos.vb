Imports System.ComponentModel

Public Class Frm_Adm_NewYearSdos
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents ButtonAperturaYear As System.Windows.Forms.Button
    Friend WithEvents LabelProgress As System.Windows.Forms.Label
    Friend WithEvents ButtonCloseLastYear As System.Windows.Forms.Button
    Friend WithEvents ButtonRevertLastYear As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ButtonExit As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonAperturaYear = New System.Windows.Forms.Button()
        Me.LabelProgress = New System.Windows.Forms.Label()
        Me.ButtonExit = New System.Windows.Forms.Button()
        Me.ButtonCloseLastYear = New System.Windows.Forms.Button()
        Me.ButtonRevertLastYear = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(72, 8)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.NumericUpDownYea.Minimum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(48, 20)
        Me.NumericUpDownYea.TabIndex = 0
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "any nou:"
        '
        'ButtonAperturaYear
        '
        Me.ButtonAperturaYear.Location = New System.Drawing.Point(130, 162)
        Me.ButtonAperturaYear.Name = "ButtonAperturaYear"
        Me.ButtonAperturaYear.Size = New System.Drawing.Size(166, 24)
        Me.ButtonAperturaYear.TabIndex = 2
        Me.ButtonAperturaYear.Text = "APERTURA D'ANY"
        '
        'LabelProgress
        '
        Me.LabelProgress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelProgress.Location = New System.Drawing.Point(8, 40)
        Me.LabelProgress.Name = "LabelProgress"
        Me.LabelProgress.Size = New System.Drawing.Size(288, 16)
        Me.LabelProgress.TabIndex = 4
        '
        'ButtonExit
        '
        Me.ButtonExit.Location = New System.Drawing.Point(8, 162)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(88, 24)
        Me.ButtonExit.TabIndex = 5
        Me.ButtonExit.Text = "CANCELAR"
        '
        'ButtonCloseLastYear
        '
        Me.ButtonCloseLastYear.Location = New System.Drawing.Point(50, 130)
        Me.ButtonCloseLastYear.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.ButtonCloseLastYear.Name = "ButtonCloseLastYear"
        Me.ButtonCloseLastYear.Size = New System.Drawing.Size(246, 25)
        Me.ButtonCloseLastYear.TabIndex = 6
        Me.ButtonCloseLastYear.Text = "TANCAMENT ANY ANTERIOR"
        '
        'ButtonRevertLastYear
        '
        Me.ButtonRevertLastYear.Location = New System.Drawing.Point(50, 104)
        Me.ButtonRevertLastYear.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.ButtonRevertLastYear.Name = "ButtonRevertLastYear"
        Me.ButtonRevertLastYear.Size = New System.Drawing.Size(246, 25)
        Me.ButtonRevertLastYear.TabIndex = 7
        Me.ButtonRevertLastYear.Text = "RETROCEDEIX TANCAMENT ANY ANTERIOR"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(8, 56)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(288, 10)
        Me.ProgressBar1.TabIndex = 8
        '
        'Frm_Adm_NewYearSdos
        '
        Me.ClientSize = New System.Drawing.Size(304, 193)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.ButtonRevertLastYear)
        Me.Controls.Add(Me.ButtonCloseLastYear)
        Me.Controls.Add(Me.ButtonExit)
        Me.Controls.Add(Me.LabelProgress)
        Me.Controls.Add(Me.ButtonAperturaYear)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumericUpDownYea)
        Me.Name = "Frm_Adm_NewYearSdos"
        Me.Text = "ARROSSEGA SALDOS A L'ANY NOU"
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mEmp As DTOEmp = BLL.BLLApp.Emp
    Private _BackGroundWorker As System.ComponentModel.BackgroundWorker

    Private Sub Frm_Adm_NewYearSdos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim CurrentYea As Integer = Today.Year
        With NumericUpDownYea
            .Minimum = CurrentYea - 2
            .Maximum = CurrentYea + 1
            .Value = CurrentYea
        End With
    End Sub

    Private Sub ButtonOberturaAny_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAperturaYear.Click
        If BLL.BLLApp.CcaIsBlockedYear(NewYear) Then
            MsgBox("any " & NewYear() & " bloqueijat!", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            ButtonAperturaYear.Enabled = False
            ButtonExit.Enabled = False

            _BackGroundWorker = New BackgroundWorker
            _BackGroundWorker.WorkerReportsProgress = True
            AddHandler _BackGroundWorker.DoWork, AddressOf AperturaYear_DoWork
            AddHandler _BackGroundWorker.ProgressChanged, AddressOf AperturaYear_ProgressChanged
            AddHandler _BackGroundWorker.RunWorkerCompleted, AddressOf AperturaYear_RunWorkerCompleted
            _BackGroundWorker.RunWorkerAsync()

        End If
    End Sub

    Private Sub AperturaYear_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim oSaldosDetall As PgcSaldos = ExerciciLoader.Saldos(OldExercici)
        Dim oSaldosResum As PgcSaldos = oSaldosDetall.ResumTotsDigits
        Dim oConn As SqlClient.SqlConnection = Current.SQLConnection(True)
        Dim oTrans As SqlClient.SqlTransaction = oConn.BeginTransaction
        Try
            ExerciciLoader.RetrocedeixAssentamentsApertura(NewExercici(), oTrans)
            SetBalanceDeApertura(oSaldosResum, oTrans)
            SetAperturaComptes(oSaldosDetall, oTrans)
            If Not PgcPlan.CheckSamePlans(OldYear, NewYear) Then
                SetPlanMigration(oTrans)
            End If
            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            MsgBox("error" & vbCrLf & ex.Message & vbCrLf & ex.StackTrace)
        Finally
            oConn.Close()
        End Try
    End Sub

    Private Sub AperturaYear_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        ProgressBar1.Value = e.ProgressPercentage
        LabelProgress.Text = e.UserState.ToString
    End Sub

    Private Sub AperturaYear_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        ButtonAperturaYear.Enabled = True
        WarnEnd()
    End Sub

    Private Sub WarnEnd()
        With LabelProgress
            .Text = "Operació finalitzada!"
            .BackColor = System.Drawing.Color.LightBlue
        End With
        ButtonExit.Text = "SORTIDA"
        ButtonExit.Enabled = True
    End Sub

    Private Sub SetBalanceDeApertura(oSaldos As PgcSaldos, ByRef oTrans As SqlClient.SqlTransaction)
        _BackGroundWorker.ReportProgress(0, "redactant balanç apertura")

        If oSaldos.Count > 0 Then

            Dim oResultat As New PgcSaldo(oSaldos(0).Exercici, PgcPlan.FromYear(OldYear).Cta(DTOPgcPlan.ctas.resultatsAnyAnterior), Nothing, New Amt(), PgcSaldo.Signes.Deutor)
            Dim oCcbs As New Ccbs()
            For Each oSaldo As PgcSaldo In oSaldos
                If oSaldo.IsNotNull And oSaldo.Cta.Id < "600" Then
                    Dim oDh As DTOCcb.DhEnum = IIf(oSaldo.IsDeutor, DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber) 'El mateix sentit que el saldo anterior
                    Dim oCcb As New Ccb(oSaldo.Cta, Nothing, oSaldo.Amt, oDh)
                    oCcbs.Add(oCcb)
                    oResultat.AddSaldo(oSaldo)
                End If
            Next

            If oResultat.IsNotNull Then
                'tanca assentament amb partida resultats
                Dim oDhResultat As DTOCcb.DhEnum = IIf(oResultat.IsDeutor, DTOCcb.DhEnum.Haber, DTOCcb.DhEnum.Debe)
                Dim oCcbResultat As New Ccb(oResultat.Cta, Nothing, oResultat.Amt.Absolute, oDhResultat)
                oCcbs.Add(oCcbResultat)
            End If

            Dim oCca As New cca(BLL.BLLApp.emp)
            With oCca
                .Ccd = DTOCca.CcdEnum.AperturaExercisi
                .Cdn = 0
                .fch = New Date(oResultat.Exercici.Yea + 1, 1, 1)
                .Txt = "Balanç d'apertura"
                .ccbs = oCcbs
                .Update(oTrans)
            End With
        End If
    End Sub


    Private Sub SetAperturaComptes(oSaldos As PgcSaldos, ByRef oTrans As SqlClient.SqlTransaction)
        Dim oSelectedSaldos As New PgcSaldos

        For Each oSaldo As PgcSaldo In oSaldos
            If oSaldo.IsNotNull Then
                If oSaldo.Cta.Id < "600" And oSaldo.Cta.Id.Substring(0, 2) <> "21" Then
                    If oSaldo.Contact IsNot Nothing Then
                        oSelectedSaldos.Add(oSaldo)
                    End If
                End If
            End If
        Next


        If oSelectedSaldos.Count > 0 Then

            Dim oLangCat As DTOLang = DTOLang.FromTag("CAT")
            Dim oCca As Cca = Nothing
            Dim oCcb As Ccb = Nothing
            Dim oResto As PgcSaldo = Nothing
            Dim oDh As DTOCcb.DhEnum

            Dim oExercici As Exercici = NewExercici()
            Dim oLastCta As New PgcCta(PgcPlan.FromToday)
            Dim idx As Integer

            For Each oSaldo As PgcSaldo In oSelectedSaldos
                idx += 1
                Dim iProgress As Integer = idx * 100 / oSelectedSaldos.Count

                If Not oSaldo.Cta.Equals(oLastCta) Then
                    If oLastCta.Id > "" Then
                        oDh = IIf(oResto.IsDeutor, DTOCcb.DhEnum.Haber, DTOCcb.DhEnum.Debe)
                        oCcb = New Ccb(oLastCta, Nothing, oResto.Amt, oDh)
                        oCca.ccbs.Add(oCcb)
                        oCca.Update(oTrans)
                    End If

                    oLastCta = oSaldo.Cta
                    oCca = New Cca(oExercici.Emp)
                    With oCca
                        .Ccd = DTOCca.CcdEnum.AperturaExercisi
                        .Cdn = oLastCta.Id
                        .fch = New Date(oExercici.Yea, 1, 1)
                        .Txt = oLastCta.FullNom(oLangCat) & "-apertura compte"
                    End With

                    oResto = New PgcSaldo(oExercici, oLastCta, Nothing, New Amt(), PgcSaldo.Signes.Deutor)
                End If

                oDh = IIf(oSaldo.IsDeutor, DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber)
                oCcb = New Ccb(oLastCta, oSaldo.Contact, oSaldo.Amt, oDh)
                oCca.ccbs.Add(oCcb)
                oResto.AddSaldo(oSaldo)

                _BackGroundWorker.ReportProgress(iProgress, oCca.Txt)
            Next

            If oLastCta.Id > "" Then
                oDh = IIf(oResto.IsDeutor, DTOCcb.DhEnum.Haber, DTOCcb.DhEnum.Debe)
                oCcb = New Ccb(oLastCta, Nothing, oResto.Amt, oDh)
                oCca.ccbs.Add(oCcb)
                oCca.Update(oTrans)
            End If

        End If

    End Sub


    Private Function NewYear() As Integer
        Return NumericUpDownYea.Value
    End Function

    Private Function OldYear() As Integer
        Return NumericUpDownYea.Value - 1
    End Function

    Private Function NewExercici() As Exercici
        Dim retval As New Exercici(BLL.BLLApp.Emp, NumericUpDownYea.Value)
        Return retval
    End Function

    Private Function OldExercici() As Exercici
        Dim retval As New Exercici(BLL.BLLApp.Emp, NumericUpDownYea.Value - 1)
        Return retval
    End Function

    Private Sub ButtonExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCloseLastYear.Click
        If BLL.BLLApp.CcaIsBlockedYear(OldYear) Then
            MsgBox("any " & OldYear() & " bloqueijat!", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            TancamentdAny()
        End If
    End Sub





    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRevertLastYear.Click
        If BLL.BLLApp.CcaIsBlockedYear(NewYear) Then
            MsgBox("any " & OldYear() & " bloqueijat!", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            Dim oExerciciAnterior As New DTOExercici
            oExerciciAnterior.Emp = mEmp
            oExerciciAnterior.Year = OldYear()
            Dim exs As New List(Of Exception)
            If BLL.BLLExercici.EliminaTancaments(oExerciciAnterior, exs) Then
                MsgBox("assentaments de tancament d'any " & OldYear() & " eliminats", MsgBoxStyle.Information, "MAT.NET")
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Public Sub TancamentdAny()
        Dim oExerciciAnterior As New Exercici(mEmp, OldYear)
        Dim iLastBlockedCcaYea As Integer = BLL.BLLDefault.EmpValue(DTODefault.Codis.LastBlockedCcaYea)

        Dim rc As MsgBoxResult
        If BLL.BLLApp.CcaIsBlockedYear(oExerciciAnterior.Yea) Then
            rc = MsgBox("L'any " & oExerciciAnterior.Yea & " está bloqueijat." & vbCrLf & "El desbloqueijem momentaneament durant aquesta operació?", MsgBoxStyle.OkCancel, "TANCAMENT D'ANY")
            If rc = MsgBoxResult.Ok Then
                BLL.BLLDefault.SetEmpValue(DTODefault.Codis.LastBlockedCcaYea, oExerciciAnterior.Yea - 1)

            Else
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "TANCAMENT D'ANY")
                Exit Sub
            End If
        End If

        Dim DtFch As Date = CDate("31/12/" & oExerciciAnterior.Yea)
        Dim exs As New List(Of Exception)
        Dim pExerciciAnterior As New DTOExercici
        With pExerciciAnterior
            .Emp = oExerciciAnterior.Emp
            .Year = oExerciciAnterior.Yea
        End With
        If BLL.BLLExercici.EliminaTancaments(pExerciciAnterior, exs) Then
            ExerciciLoader.TancamentdeSubComptes(oExerciciAnterior)
            ExerciciLoader.TancamentExplotacio(oExerciciAnterior)
            Dim DcSdo As Decimal = TancamentBalanç(oExerciciAnterior.Yea)

            'restaura la protecció de bloqueig de l'any
            If iLastBlockedCcaYea >= oExerciciAnterior.Yea Then
                BLL.BLLDefault.SetEmpValue(DTODefault.Codis.LastBlockedCcaYea, iLastBlockedCcaYea)
            End If

            If Math.Round(DcSdo, 2) <> 0 Then
                MsgBox("no cuadra per " & Format(DcSdo, "#,##0.00") & " EUR", MsgBoxStyle.Exclamation, "mat.net")
            Else
                MsgBox("Operació finalitzada satisfactoriament" & vbCrLf & "Cal renumerar els assentaments", MsgBoxStyle.Information, "MAT.NET")
            End If
            Me.Close()
        Else
            UIHelper.WarnError(exs)

        End If
    End Sub

    Private Function TancamentBalanç(ByVal iYea As Integer) As Decimal
        Dim SQL As String = "SELECT CCB.PGCPLAN, CCB.cta, Ccb.CtaGuid, PGCCTA.ESP AS dsc, " _
        & "SUM(CASE WHEN CCB.DH = 1 THEN EUR ELSE 0 END) AS DEB, " _
        & "SUM(CASE WHEN CCB.DH = 2 THEN EUR ELSE 0 END) AS HAB " _
        & "FROM CCB LEFT OUTER JOIN " _
        & "PGCCTA ON Ccb.CtaGuid = PgcCta.Guid " _
        & "WHERE CCB.Emp =" & mEmp.Id & " AND " _
        & "CCB.yea =" & iYea & " AND " _
        & "CCB.CTA < '6' " _
        & "GROUP BY CCB.PGCPLAN,CCB.cta, Ccb.CtaGuid, PGCCTA.ESP " _
        & "HAVING SUM(CASE WHEN CCB.DH = 1 THEN EUR ELSE 0 END) <> SUM(CASE WHEN CCB.DH = 2 THEN EUR ELSE 0 END) " _
        & "ORDER BY CCB.cta"

        Dim oCca As Cca = Nothing
        Dim oContact As Contact = Nothing
        Dim oCcb As Ccb = Nothing
        Dim oCta As PgcCta
        Dim oAmt As maxisrvr.Amt
        Dim oDh As DTOCcb.DhEnum
        Dim DcEur As Decimal
        Dim Firstrec As Boolean = True
        Dim DcSdo As Decimal

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            If Firstrec Then
                Firstrec = False
                oCca = New cca(BLL.BLLApp.emp)
                With oCca
                    .Ccd = DTOCca.CcdEnum.TancamentExplotacio
                    .fch = "31/12/" & iYea
                    .Txt = "balanç de tancament"
                End With
            End If

            Dim oPlan As New PgcPlan(oRow("PgcPlan"))
            oCta = New PgcCta(CType(oRow("CtaGuid"), Guid))
            oCta.Plan = oPlan

            oDh = IIf(oRow("DEB") > oRow("HAB"), DTOCcb.DhEnum.Haber, DTOCcb.DhEnum.Debe)
            DcEur = Math.Abs(Math.Round(oRow("DEB") - oRow("HAB"), 2))
            oAmt = New maxisrvr.Amt(DcEur)
            oCcb = New Ccb(oCta, oContact, oAmt, oDh)
            oCca.ccbs.Add(oCcb)
            DcSdo += IIf(oDh = DTOCcb.DhEnum.Debe, -DcEur, DcEur)
        Next

        If Not Firstrec Then
            Dim exs as New List(Of exception)
            If Not oCca.Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End If
        Return DcSdo
    End Function

    Private Sub SetPlanMigration(ByRef oTrans As SqlClient.SqlTransaction)
        Dim oOldPlan As PgcPlan = PgcPlan.FromYear(OldYear)
        Dim oNewPlan As PgcPlan = PgcPlan.FromYear(NewYear)
        Dim oOldCta As PgcCta = Nothing
        Dim oNewCta As PgcCta = Nothing
        Dim DtNewYearFch As New Date(NewYear, 1, 1)
        Dim sNewYearFch As String = Format(DtNewYearFch, "yyyyMMdd")
        Dim oCca As Cca
        Dim DecSdoPts As Decimal = 0
        Dim DecSdoEur As Decimal = 0
        Dim oContact As Contact = Nothing
        Dim oCur As MaxiSrvr.Cur
        Dim oAmt As MaxiSrvr.Amt
        Dim DhNew As DTOCcb.DhEnum
        Dim DhOld As DTOCcb.DhEnum

        Dim SQL As String = "SELECT CCB.cta AS OLDCTA, CCB.cli, CCB.CUR, PGCCTA.NextCta AS NEWCTA, " _
        & "SUM(CASE WHEN DH = 1 THEN PTS ELSE - PTS END) AS SDODEBPTS, " _
        & "SUM(CASE WHEN DH = 1 THEN EUR ELSE - EUR END) AS SDODEBEUR " _
        & "FROM CCA INNER JOIN " _
        & "CCB ON Ccb.CcaGuid = Cca.Guid LEFT OUTER JOIN " _
        & "PGCCTA ON CCB.PgcPlan = PGCCTA.PgcPlan AND CCB.cta LIKE PGCCTA.Id " _
        & "WHERE CCB.EMP=" & mEmp.Id & " AND " _
        & "CCB.yea =" & NewYear() & " AND " _
        & "CCA.ccd =" & DTOCca.CcdEnum.AperturaExercisi & " AND " _
        & "CCB.PgcPlan =" & oOldPlan.Id & " AND " _
        & "CCB.fch = '" & sNewYearFch & "' " _
        & "GROUP BY CCB.cta, CCB.cli, CCB.CUR, PGCCTA.NextCta " _
        & "HAVING (SUM(CASE WHEN DH = 1 THEN EUR ELSE - EUR END) <> 0) " _
        & "ORDER BY OLDCTA, CCB.CUR"

        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
        Do While oDrd.Read
            oOldCta = MaxiSrvr.PgcCta.FromNum(oOldPlan, oDrd("OLDCTA"))
            oNewCta = MaxiSrvr.PgcCta.FromNum(oNewPlan, oDrd("NEWCTA"))
            DecSdoPts = oDrd("SDODEBPTS")
            DecSdoEur = oDrd("SDODEBEUR")
            oCur = Current.Cur(oDrd("Cur"))
            oAmt = New MaxiSrvr.Amt(DecSdoPts, oCur, DecSdoEur).Absolute
            DhOld = IIf(DecSdoEur > 0, DTOCcb.DhEnum.Haber, DTOCcb.DhEnum.Debe)
            DhNew = IIf(DecSdoEur > 0, DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber)
            oContact = MaxiSrvr.Contact.FromNum(mEmp, oDrd("CLI"))
            oCca = New cca(BLL.BLLApp.emp)
            With oCca
                .Ccd = DTOCca.CcdEnum.MigracioPlaComptable
                .Cdn = oOldCta.Id
                .fch = DtNewYearFch
                .Txt = "migració de pla comptable " & oOldPlan.Nom & " a " & oNewPlan.Nom
                .ccbs.Add(New Ccb(oOldCta, oContact, oAmt, DhOld))
                .ccbs.Add(New Ccb(oNewCta, oContact, oAmt, DhNew))
                .Update(oTrans)
            End With
        Loop
        oDrd.Close()
    End Sub

End Class
