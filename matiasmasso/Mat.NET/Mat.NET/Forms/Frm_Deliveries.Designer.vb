<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Deliveries
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
        Me.Xl_Deliveries1 = New Mat.NET.Xl_Deliveries()
        Me.Xl_Years1 = New Mat.NET.Xl_Years()
        Me.LabelFiltre = New System.Windows.Forms.Label()
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        CType(Me.Xl_Deliveries1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Deliveries1
        '
        Me.Xl_Deliveries1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Deliveries1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Deliveries1.Filter = Nothing
        Me.Xl_Deliveries1.Location = New System.Drawing.Point(3, 33)
        Me.Xl_Deliveries1.Name = "Xl_Deliveries1"
        Me.Xl_Deliveries1.Size = New System.Drawing.Size(767, 228)
        Me.Xl_Deliveries1.TabIndex = 0
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(4, 4)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 8
        Me.Xl_Years1.Value = 0
        '
        'LabelFiltre
        '
        Me.LabelFiltre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelFiltre.AutoSize = True
        Me.LabelFiltre.Location = New System.Drawing.Point(446, 10)
        Me.LabelFiltre.Name = "LabelFiltre"
        Me.LabelFiltre.Size = New System.Drawing.Size(29, 13)
        Me.LabelFiltre.TabIndex = 7
        Me.LabelFiltre.Text = "filtre:"
        Me.LabelFiltre.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearch.ForeColor = System.Drawing.Color.Gray
        Me.TextBoxSearch.Location = New System.Drawing.Point(479, 7)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(291, 20)
        Me.TextBoxSearch.TabIndex = 6
        '
        'Frm_Deliveries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 261)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.LabelFiltre)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.Xl_Deliveries1)
        Me.Name = "Frm_Deliveries"
        Me.Text = "Albarans"
        CType(Me.Xl_Deliveries1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Deliveries1 As Mat.NET.Xl_Deliveries
    Friend WithEvents Xl_Years1 As Mat.NET.Xl_Years
    Friend WithEvents LabelFiltre As System.Windows.Forms.Label
    Friend WithEvents TextBoxSearch As System.Windows.Forms.TextBox
End Class
