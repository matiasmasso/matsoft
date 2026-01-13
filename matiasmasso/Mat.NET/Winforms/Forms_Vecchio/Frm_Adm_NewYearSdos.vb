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

            Dim oExercici As DTOExercici = BLLExercici.FromYear(NewYear)
            Dim exs As New List(Of Exception)
            If BLLExercici.Apertura(oExercici, BLLSession.Current.User, AddressOf ReportProgress, exs) Then
                ButtonAperturaYear.Enabled = True
                WarnEnd()
            Else
                UIHelper.WarnError(exs, "error al generar la apertura d'exercici")
                Me.Close()
            End If

        End If
    End Sub


    Private Sub ReportProgress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByRef label As String, ByRef CancelRequest As Boolean)
        With ProgressBar1
            ProgressBar1.Minimum = min
            ProgressBar1.Maximum = max
            ProgressBar1.Value = value
        End With
        LabelProgress.Text = label
        Application.DoEvents()
    End Sub

    Private Sub WarnEnd()
        With LabelProgress
            .Text = "Operació finalitzada!"
            .BackColor = System.Drawing.Color.LightBlue
        End With
        ButtonExit.Text = "SORTIDA"
        ButtonExit.Enabled = True
    End Sub

    Private Function NewYear() As Integer
        Return NumericUpDownYea.Value
    End Function

    Private Function OldYear() As Integer
        Return NumericUpDownYea.Value - 1
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
            Dim oExerciciAnterior As New DTOExercici(mEmp, OldYear)
            Dim exs As New List(Of Exception)
            If BLL.BLLExercici.EliminaTancaments(oExerciciAnterior, exs) Then
                MsgBox("assentaments de tancament d'any " & OldYear() & " eliminats", MsgBoxStyle.Information, "MAT.NET")
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Public Sub TancamentdAny()
        Dim oExerciciAnterior As New DTOExercici(mEmp, OldYear)
        Dim iLastBlockedCcaYea As Integer = BLL.BLLDefault.EmpValue(DTODefault.Codis.LastBlockedCcaYea)

        Dim rc As MsgBoxResult
        If BLL.BLLApp.CcaIsBlockedYear(oExerciciAnterior.Year) Then
            rc = MsgBox("L'any " & oExerciciAnterior.Year & " está bloqueijat." & vbCrLf & "El desbloqueijem momentaneament durant aquesta operació?", MsgBoxStyle.OkCancel, "TANCAMENT D'ANY")
            If rc = MsgBoxResult.Ok Then
                BLL.BLLDefault.SetEmpValue(DTODefault.Codis.LastBlockedCcaYea, oExerciciAnterior.Year - 1)

            Else
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "TANCAMENT D'ANY")
                Exit Sub
            End If
        End If

        Dim DtFch As Date = CDate("31/12/" & oExerciciAnterior.Year)
        Dim exs As New List(Of Exception)
        Dim pExerciciAnterior As New DTOExercici(oExerciciAnterior.Emp, oExerciciAnterior.Year)
        If BLL.BLLExercici.EliminaTancaments(pExerciciAnterior, exs) Then
            ExerciciLoader.TancamentdeSubComptes(oExerciciAnterior)
            ExerciciLoader.TancamentExplotacio(oExerciciAnterior)
            Dim DcSdo As Decimal = TancamentBalanç(oExerciciAnterior.Year)

            'restaura la protecció de bloqueig de l'any
            If iLastBlockedCcaYea >= oExerciciAnterior.Year Then
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
        Dim SQL As String = "SELECT CCB.PGCPLAN, PgcCta.Id AS Cta, Ccb.CtaGuid, PGCCTA.ESP AS dsc, " _
        & "SUM(CASE WHEN CCB.DH = 1 THEN EUR ELSE 0 END) AS DEB, " _
        & "SUM(CASE WHEN CCB.DH = 2 THEN EUR ELSE 0 END) AS HAB " _
        & "FROM CCB LEFT OUTER JOIN " _
        & "PGCCTA ON Ccb.CtaGuid = PgcCta.Guid " _
        & "WHERE CCB.Emp =" & mEmp.Id & " AND " _
        & "CCB.yea =" & iYea & " AND " _
        & "PgcCta.Id < '6' " _
        & "GROUP BY CCB.PGCPLAN, PgcCta.Id, Ccb.CtaGuid, PGCCTA.ESP " _
        & "HAVING SUM(CASE WHEN CCB.DH = 1 THEN EUR ELSE 0 END) <> SUM(CASE WHEN CCB.DH = 2 THEN EUR ELSE 0 END) " _
        & "ORDER BY PgcCta.Id"

        Dim oCca As Cca = Nothing
        Dim oContact As Contact = Nothing
        Dim oCcb As Ccb = Nothing
        Dim oCta As PgcCta
        Dim oAmt As DTOAmt
        Dim oDh As DTOCcb.DhEnum
        Dim DcEur As Decimal
        Dim Firstrec As Boolean = True
        Dim DcSdo As Decimal

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            If Firstrec Then
                Firstrec = False
                oCca = New Cca(BLL.BLLApp.Emp)
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
            oAmt = BLLApp.GetAmt(DcEur)
            oCcb = New Ccb(oCta, oContact, oAmt, oDh)
            oCca.ccbs.Add(oCcb)
            DcSdo += IIf(oDh = DTOCcb.DhEnum.Debe, -DcEur, DcEur)
        Next

        If Not Firstrec Then
            Dim exs As New List(Of Exception)
            If Not oCca.Update(exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End If
        Return DcSdo
    End Function


End Class
