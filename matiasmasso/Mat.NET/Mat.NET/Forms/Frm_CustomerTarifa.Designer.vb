<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CustomerTarifa
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ButtonRefresh = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Tarifa1 = New Mat.NET.Xl_Tarifa()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_CustomerDtos1 = New Mat.NET.Xl_CustomerTarifaDtos()
        Me.Xl_CliProductsBlocked1 = New Mat.NET.Xl_CliProductsBlocked()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(320, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(95, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'ButtonRefresh
        '
        Me.ButtonRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefresh.Location = New System.Drawing.Point(417, 11)
        Me.ButtonRefresh.Name = "ButtonRefresh"
        Me.ButtonRefresh.Size = New System.Drawing.Size(28, 22)
        Me.ButtonRefresh.TabIndex = 1
        Me.ButtonRefresh.Text = "..."
        Me.ButtonRefresh.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 46)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(454, 491)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Tarifa1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(446, 465)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Preus"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Tarifa1
        '
        Me.Xl_Tarifa1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Tarifa1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Tarifa1.Name = "Xl_Tarifa1"
        Me.Xl_Tarifa1.Size = New System.Drawing.Size(440, 459)
        Me.Xl_Tarifa1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_CustomerDtos1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(446, 465)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Descomptes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_CliProductsBlocked1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(446, 465)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Exclusives"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_CustomerDtos1
        '
        Me.Xl_CustomerDtos1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CustomerDtos1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_CustomerDtos1.Name = "Xl_CustomerDtos1"
        Me.Xl_CustomerDtos1.Size = New System.Drawing.Size(440, 459)
        Me.Xl_CustomerDtos1.TabIndex = 0
        '
        'Xl_CliProductsBlocked1
        '
        Me.Xl_CliProductsBlocked1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CliProductsBlocked1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_CliProductsBlocked1.Name = "Xl_CliProductsBlocked1"
        Me.Xl_CliProductsBlocked1.Size = New System.Drawing.Size(440, 459)
        Me.Xl_CliProductsBlocked1.TabIndex = 0
        '
        'Frm_CustomerTarifa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 536)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ButtonRefresh)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Name = "Frm_CustomerTarifa"
        Me.Text = "Tarifas"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ButtonRefresh As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Tarifa1 As Mat.NET.Xl_Tarifa
    Friend WithEvents Xl_CustomerDtos1 As Mat.NET.Xl_CustomerTarifaDtos
    Friend WithEvents Xl_CliProductsBlocked1 As Mat.NET.Xl_CliProductsBlocked
End Class
