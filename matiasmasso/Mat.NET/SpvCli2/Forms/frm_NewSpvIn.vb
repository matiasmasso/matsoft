Imports System.io

Public Class frm_NewSpvIn

    Inherits System.Windows.Forms.Form

    Friend WithEvents DateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxExp As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBts As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxM3 As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button

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
    Friend WithEvents CheckedListBox As System.Windows.Forms.CheckedListBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_NewSpvIn))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxBts = New System.Windows.Forms.TextBox()
        Me.TextBoxM3 = New System.Windows.Forms.TextBox()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.DateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.CheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.TextBoxExp = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(608, 445)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 15)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "&Vol:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.Location = New System.Drawing.Point(16, 443)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 17)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "&Observacions:"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(368, 445)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "&hora:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(472, 445)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "&expedición:"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(560, 443)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "&bultos:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBoxBts
        '
        Me.TextBoxBts.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBts.Location = New System.Drawing.Point(560, 460)
        Me.TextBoxBts.Name = "TextBoxBts"
        Me.TextBoxBts.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxBts.TabIndex = 6
        Me.TextBoxBts.Text = "0"
        Me.TextBoxBts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxM3
        '
        Me.TextBoxM3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxM3.Location = New System.Drawing.Point(600, 460)
        Me.TextBoxM3.Name = "TextBoxM3"
        Me.TextBoxM3.Size = New System.Drawing.Size(48, 20)
        Me.TextBoxM3.TabIndex = 8
        Me.TextBoxM3.Text = "0"
        Me.TextBoxM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(16, 460)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(344, 63)
        Me.TextBoxObs.TabIndex = 10
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.Location = New System.Drawing.Point(368, 499)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(96, 24)
        Me.BtnCancel.TabIndex = 11
        Me.BtnCancel.TabStop = False
        Me.BtnCancel.Text = "&CANCELAR"
        '
        'DateTimePicker
        '
        Me.DateTimePicker.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker.CustomFormat = "dd/MM/yy HH:mm"
        Me.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker.Location = New System.Drawing.Point(368, 460)
        Me.DateTimePicker.Name = "DateTimePicker"
        Me.DateTimePicker.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker.TabIndex = 2
        Me.DateTimePicker.TabStop = False
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Location = New System.Drawing.Point(560, 499)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(88, 24)
        Me.ButtonEnd.TabIndex = 13
        Me.ButtonEnd.TabStop = False
        Me.ButtonEnd.Text = "&FIN"
        '
        'BtnOk
        '
        Me.BtnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOk.Location = New System.Drawing.Point(464, 499)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(96, 24)
        Me.BtnOk.TabIndex = 12
        Me.BtnOk.Text = "&ACCEPTAR"
        '
        'CheckedListBox
        '
        Me.CheckedListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBox.CheckOnClick = True
        Me.CheckedListBox.Location = New System.Drawing.Point(16, 128)
        Me.CheckedListBox.Name = "CheckedListBox"
        Me.CheckedListBox.Size = New System.Drawing.Size(632, 304)
        Me.CheckedListBox.TabIndex = 0
        '
        'TextBoxExp
        '
        Me.TextBoxExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExp.Location = New System.Drawing.Point(464, 460)
        Me.TextBoxExp.Name = "TextBoxExp"
        Me.TextBoxExp.Size = New System.Drawing.Size(96, 20)
        Me.TextBoxExp.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.SpvCli2.My.Resources.Resources.unpack
        Me.PictureBox1.Location = New System.Drawing.Point(600, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(19, 10)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(570, 96)
        Me.TextBox1.TabIndex = 15
        Me.TextBox1.Text = resources.GetString("TextBox1.Text")
        '
        'frm_NewSpvIn
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(656, 528)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxM3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxBts)
        Me.Controls.Add(Me.TextBoxExp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePicker)
        Me.Controls.Add(Me.CheckedListBox)
        Me.Name = "frm_NewSpvIn"
        Me.Text = "ENTRADA DE REPARACIONS"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _Spvs As List(Of DTOSpv)

    Private Async Sub frm_NewSpvIn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor = Cursors.WaitCursor
        DateTimePicker.Value = Now

        Dim exs As New List(Of Exception)
        _Spvs = Await FEB2.Spvs.ArrivalPending(Current.Session.User, exs)
        Cursor = Cursors.Default
        If exs.Count = 0 Then
            CheckedListBox.Items.Clear()
            For Each itm As DTOSpv In _Spvs
                Dim Str As String = itm.Id & " " & itm.FchAvis & " - " & itm.Product.Nom & " - " & itm.Customer.FullNom
                CheckedListBox.Items.Add(Str, False)
            Next
            TextBoxExp.Text = ""
            TextBoxBts.Text = ""
            TextBoxM3.Text = ""
            TextBoxObs.Text = ""
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim exs As New List(Of Exception)

        If TextBoxBts.Text = "" Then
            MsgBox("faltan bultos", MsgBoxStyle.Exclamation, "M+O Servei Tecnic")
            Exit Sub
        End If

        Dim sM3 As String = TextBoxM3.Text
        If sM3 = "" Then
            MsgBox("Falta el volum", MsgBoxStyle.Exclamation, "M+O Servei Tecnic")
            Exit Sub
        ElseIf IsNumeric(sM3) Then
            If CInt(sM3) > 1 Then
                Dim rc As MsgBoxResult = MsgBox("El volum declarat sembla molt alt." & vbCrLf & "Prem Cancelar per rectificar-lo o Acceptar per confirmar-lo.", MsgBoxStyle.OkCancel, "M+O Servei Tecnic")
            End If
        Else
            MsgBox("Volum no valid", MsgBoxStyle.Exclamation, "M+O Servei Tecnic")
            Exit Sub
        End If

        Dim oSpvs As List(Of DTOSpv) = CheckedSpvs()
        If oSpvs.Count > 0 Then
            Dim oSpvIn = DTOSpvIn.Factory(Current.Session.User, DateTimePicker.Value)
            With oSpvIn
                .Expedicio = TextBoxExp.Text
                .Bultos = TextBoxBts.Text
                .M3 = TextBoxM3.Text
                .Obs = TextBoxObs.Text
                .Spvs = oSpvs
            End With

            Dim id = Await FEB2.SpvIn.Update(oSpvIn, exs)
            If exs.Count = 0 Then
                oSpvIn.Id = id
                MsgBox("Entrada nº " & oSpvIn.Id, MsgBoxStyle.Information, "M+O Servei Tecnic")

                For i = _Spvs.Count - 1 To 0 Step -1
                    If CheckedListBox.GetItemChecked(i) = True Then
                        CheckedListBox.Items.RemoveAt(i)
                        _Spvs.RemoveAt(i)
                    End If
                Next

                TextBoxExp.Text = ""
                TextBoxBts.Text = ""
                TextBoxM3.Text = ""
                TextBoxObs.Text = ""
            Else
                UIHelper.WarnError(exs)
            End If

        End If
    End Sub


    Private Sub FormValidation()
        Dim BlTemp As Boolean
        BlTemp = True
        If TextBoxBts.Text = "" Then BlTemp = False
        If CheckedListBox.CheckedItems.Count = 0 Then BlTemp = False
        If BlTemp = True Then BtnOk.Enabled = True
    End Sub

    Private Sub CheckedListBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBox.Click
        FormValidation()
    End Sub

    Private Sub TextBoxBts_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxBts.VisibleChanged
        FormValidation()
    End Sub


    Private Sub ButtonEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        BtnOk_Click(sender, e)
        Me.Close()
    End Sub

    Private Function ValidatePage() As Boolean
        ValidatePage = True
        If TextBoxExp.Text = "" Then ValidatePage = False
        If TextBoxBts.Text = "" Then ValidatePage = False
        If TextBoxM3.Text = "" Then ValidatePage = False
        If CheckedListBox.CheckedItems.Count = 0 Then ValidatePage = False
        If ValidatePage = True Then BtnOk.Enabled = True
    End Function

    Private Function CheckedSpvs() As List(Of DTOSpv)
        Dim oSpvs As New List(Of DTOSpv)
        Dim i As Integer
        For i = _Spvs.Count - 1 To 0 Step -1
            If CheckedListBox.GetItemChecked(i) = True Then
                oSpvs.Add(_Spvs(i))
                _Spvs.RemoveAt(i)
                CheckedListBox.Items.RemoveAt(i)
            End If
        Next
        Return oSpvs
    End Function

    Private Sub CheckedListBox_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox.ItemCheck
        ValidatePage()
    End Sub

    Private Sub TextBoxExp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxExp.TextChanged
        ValidatePage()
    End Sub
    Private Sub TextBoxBts_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxExp.TextChanged
        ValidatePage()
    End Sub
    Private Sub TextBoxKgs_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxExp.TextChanged
        ValidatePage()
    End Sub



    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub TextBoxM3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxM3.KeyPress
        If e.KeyChar = "." Then e.KeyChar = ","
    End Sub


End Class
