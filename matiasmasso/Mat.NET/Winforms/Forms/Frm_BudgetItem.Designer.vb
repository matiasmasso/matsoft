<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BudgetItem
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxBudget = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxYear = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_AmountPendent = New Winforms.Xl_Amount()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmountAplicat = New Winforms.Xl_Amount()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_AmountAssignat = New Winforms.Xl_Amount()
        Me.Xl_MonthTo = New Winforms.Xl_Month()
        Me.Xl_MonthFrom = New Winforms.Xl_Month()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_DocfileSrcs1 = New Winforms.Xl_DocfileSrcs()
        Me.Xl_BudgetItemTickets1 = New Winforms.Xl_BudgetItemTickets()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_DocfileSrcs1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_BudgetItemTickets1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 283)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(424, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(205, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(316, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(6, 39)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(414, 243)
        Me.TabControl1.TabIndex = 43
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxBudget)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxYear)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Xl_AmountPendent)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Xl_AmountAplicat)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Xl_AmountAssignat)
        Me.TabPage1.Controls.Add(Me.Xl_MonthTo)
        Me.TabPage1.Controls.Add(Me.Xl_MonthFrom)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(406, 217)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(114, 13)
        Me.Label7.TabIndex = 64
        Me.Label7.Text = "Partida pressupostaria:"
        '
        'TextBoxBudget
        '
        Me.TextBoxBudget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBudget.Location = New System.Drawing.Point(6, 35)
        Me.TextBoxBudget.Name = "TextBoxBudget"
        Me.TextBoxBudget.ReadOnly = True
        Me.TextBoxBudget.Size = New System.Drawing.Size(394, 20)
        Me.TextBoxBudget.TabIndex = 63
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(26, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Exercici"
        '
        'TextBoxYear
        '
        Me.TextBoxYear.Location = New System.Drawing.Point(83, 96)
        Me.TextBoxYear.Name = "TextBoxYear"
        Me.TextBoxYear.ReadOnly = True
        Me.TextBoxYear.Size = New System.Drawing.Size(82, 20)
        Me.TextBoxYear.TabIndex = 11
        Me.TextBoxYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(227, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Pendent"
        '
        'Xl_AmountPendent
        '
        Me.Xl_AmountPendent.Amt = Nothing
        Me.Xl_AmountPendent.Location = New System.Drawing.Point(284, 151)
        Me.Xl_AmountPendent.Name = "Xl_AmountPendent"
        Me.Xl_AmountPendent.ReadOnly = True
        Me.Xl_AmountPendent.Size = New System.Drawing.Size(82, 20)
        Me.Xl_AmountPendent.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(227, 125)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Aplicat"
        '
        'Xl_AmountAplicat
        '
        Me.Xl_AmountAplicat.Amt = Nothing
        Me.Xl_AmountAplicat.Location = New System.Drawing.Point(284, 123)
        Me.Xl_AmountAplicat.Name = "Xl_AmountAplicat"
        Me.Xl_AmountAplicat.ReadOnly = True
        Me.Xl_AmountAplicat.Size = New System.Drawing.Size(82, 20)
        Me.Xl_AmountAplicat.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(227, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Import"
        '
        'Xl_AmountAssignat
        '
        Me.Xl_AmountAssignat.Amt = Nothing
        Me.Xl_AmountAssignat.Location = New System.Drawing.Point(284, 96)
        Me.Xl_AmountAssignat.Name = "Xl_AmountAssignat"
        Me.Xl_AmountAssignat.ReadOnly = False
        Me.Xl_AmountAssignat.Size = New System.Drawing.Size(82, 20)
        Me.Xl_AmountAssignat.TabIndex = 5
        '
        'Xl_MonthTo
        '
        Me.Xl_MonthTo.FormattingEnabled = True
        Me.Xl_MonthTo.Location = New System.Drawing.Point(83, 150)
        Me.Xl_MonthTo.Month = 0
        Me.Xl_MonthTo.Name = "Xl_MonthTo"
        Me.Xl_MonthTo.Size = New System.Drawing.Size(82, 21)
        Me.Xl_MonthTo.TabIndex = 4
        '
        'Xl_MonthFrom
        '
        Me.Xl_MonthFrom.FormattingEnabled = True
        Me.Xl_MonthFrom.Location = New System.Drawing.Point(83, 122)
        Me.Xl_MonthFrom.Month = 0
        Me.Xl_MonthFrom.Name = "Xl_MonthFrom"
        Me.Xl_MonthFrom.Size = New System.Drawing.Size(82, 21)
        Me.Xl_MonthFrom.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Final"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Inici"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_BudgetItemTickets1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(406, 217)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Factures"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_DocfileSrcs1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(406, 217)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Descarregues"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_DocfileSrcs1
        '
        Me.Xl_DocfileSrcs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DocfileSrcs1.DisplayObsolets = False
        Me.Xl_DocfileSrcs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_DocfileSrcs1.Filter = Nothing
        Me.Xl_DocfileSrcs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_DocfileSrcs1.MouseIsDown = False
        Me.Xl_DocfileSrcs1.Name = "Xl_DocfileSrcs1"
        Me.Xl_DocfileSrcs1.Size = New System.Drawing.Size(400, 211)
        Me.Xl_DocfileSrcs1.TabIndex = 0
        '
        'Xl_BudgetItemTickets1
        '
        Me.Xl_BudgetItemTickets1.AllowUserToAddRows = False
        Me.Xl_BudgetItemTickets1.AllowUserToDeleteRows = False
        Me.Xl_BudgetItemTickets1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BudgetItemTickets1.DisplayObsolets = False
        Me.Xl_BudgetItemTickets1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BudgetItemTickets1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_BudgetItemTickets1.MouseIsDown = False
        Me.Xl_BudgetItemTickets1.Name = "Xl_BudgetItemTickets1"
        Me.Xl_BudgetItemTickets1.ReadOnly = True
        Me.Xl_BudgetItemTickets1.Size = New System.Drawing.Size(400, 211)
        Me.Xl_BudgetItemTickets1.TabIndex = 0
        '
        'Frm_BudgetItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 314)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_BudgetItem"
        Me.Text = "Pressupostos: Acció"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_DocfileSrcs1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_BudgetItemTickets1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_AmountPendent As Xl_Amount
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_AmountAplicat As Xl_Amount
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_AmountAssignat As Xl_Amount
    Friend WithEvents Xl_MonthTo As Xl_Month
    Friend WithEvents Xl_MonthFrom As Xl_Month
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxYear As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxBudget As TextBox
    Friend WithEvents Xl_DocfileSrcs1 As Xl_DocfileSrcs
    Friend WithEvents Xl_BudgetItemTickets1 As Xl_BudgetItemTickets
End Class
