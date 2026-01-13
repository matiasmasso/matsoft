<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_YearMonth
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.NumericUpDownYear = New System.Windows.Forms.NumericUpDown()
        Me.ComboBoxMonth = New System.Windows.Forms.ComboBox()
        CType(Me.NumericUpDownYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumericUpDownYear
        '
        Me.NumericUpDownYear.Location = New System.Drawing.Point(0, 0)
        Me.NumericUpDownYear.Maximum = New Decimal(New Integer() {2050, 0, 0, 0})
        Me.NumericUpDownYear.Minimum = New Decimal(New Integer() {1985, 0, 0, 0})
        Me.NumericUpDownYear.Name = "NumericUpDownYear"
        Me.NumericUpDownYear.Size = New System.Drawing.Size(46, 20)
        Me.NumericUpDownYear.TabIndex = 0
        Me.NumericUpDownYear.Value = New Decimal(New Integer() {2013, 0, 0, 0})
        '
        'ComboBoxMonth
        '
        Me.ComboBoxMonth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxMonth.FormattingEnabled = True
        Me.ComboBoxMonth.Location = New System.Drawing.Point(47, 0)
        Me.ComboBoxMonth.Name = "ComboBoxMonth"
        Me.ComboBoxMonth.Size = New System.Drawing.Size(103, 21)
        Me.ComboBoxMonth.TabIndex = 1
        '
        'Xl_YearMonth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ComboBoxMonth)
        Me.Controls.Add(Me.NumericUpDownYear)
        Me.Name = "Xl_YearMonth"
        Me.Size = New System.Drawing.Size(150, 19)
        CType(Me.NumericUpDownYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NumericUpDownYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBoxMonth As System.Windows.Forms.ComboBox

End Class
