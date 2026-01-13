<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_HoldingInvrpt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Xl_HoldingInvrpt))
        Me.ComboBoxMode = New System.Windows.Forms.ComboBox()
        Me.Xl_InvRpts1 = New Mat.Net.Xl_InvRpts()
        Me.Xl_LookupFch1 = New Mat.Net.Xl_LookupFch()
        CType(Me.Xl_InvRpts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxMode
        '
        Me.ComboBoxMode.FormattingEnabled = True
        Me.ComboBoxMode.Items.AddRange(New Object() {"per producte", "per centre"})
        Me.ComboBoxMode.Location = New System.Drawing.Point(4, 4)
        Me.ComboBoxMode.Name = "ComboBoxMode"
        Me.ComboBoxMode.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxMode.TabIndex = 1
        '
        'Xl_InvRpts1
        '
        Me.Xl_InvRpts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_InvRpts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InvRpts1.DisplayObsolets = False
        Me.Xl_InvRpts1.Location = New System.Drawing.Point(3, 31)
        Me.Xl_InvRpts1.MouseIsDown = False
        Me.Xl_InvRpts1.Name = "Xl_InvRpts1"
        Me.Xl_InvRpts1.Size = New System.Drawing.Size(413, 199)
        Me.Xl_InvRpts1.TabIndex = 0
        '
        'Xl_LookupFch1
        '
        Me.Xl_LookupFch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupFch1.AvailableFchs = CType(resources.GetObject("Xl_LookupFch1.AvailableFchs"), System.Collections.Generic.List(Of Date))
        Me.Xl_LookupFch1.Fch = New Date(CType(0, Long))
        Me.Xl_LookupFch1.IsDirty = False
        Me.Xl_LookupFch1.Location = New System.Drawing.Point(299, 5)
        Me.Xl_LookupFch1.Name = "Xl_LookupFch1"
        Me.Xl_LookupFch1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupFch1.ReadOnlyLookup = False
        Me.Xl_LookupFch1.Size = New System.Drawing.Size(113, 20)
        Me.Xl_LookupFch1.TabIndex = 2
        Me.Xl_LookupFch1.Value = Nothing
        '
        'Xl_HoldingInvrpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_LookupFch1)
        Me.Controls.Add(Me.ComboBoxMode)
        Me.Controls.Add(Me.Xl_InvRpts1)
        Me.Name = "Xl_HoldingInvrpt"
        Me.Size = New System.Drawing.Size(419, 230)
        CType(Me.Xl_InvRpts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_InvRpts1 As Xl_InvRpts
    Friend WithEvents ComboBoxMode As ComboBox
    Friend WithEvents Xl_LookupFch1 As Xl_LookupFch
End Class
