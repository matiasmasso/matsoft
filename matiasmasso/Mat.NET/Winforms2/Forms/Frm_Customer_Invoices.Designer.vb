<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Customer_Invoices
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
        Me.Xl_Invoices1 = New Xl_Invoices()
        Me.CheckBoxDelivered = New System.Windows.Forms.CheckBox()
        Me.CheckBoxDeliverPending = New System.Windows.Forms.CheckBox()
        CType(Me.Xl_Invoices1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Invoices1
        '
        Me.Xl_Invoices1.AllowUserToAddRows = False
        Me.Xl_Invoices1.AllowUserToDeleteRows = False
        Me.Xl_Invoices1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Invoices1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Invoices1.DisplayObsolets = False
        Me.Xl_Invoices1.Filter = Nothing
        Me.Xl_Invoices1.Location = New System.Drawing.Point(0, 58)
        Me.Xl_Invoices1.MouseIsDown = False
        Me.Xl_Invoices1.Name = "Xl_Invoices1"
        Me.Xl_Invoices1.ReadOnly = True
        Me.Xl_Invoices1.Size = New System.Drawing.Size(486, 203)
        Me.Xl_Invoices1.TabIndex = 0
        '
        'CheckBoxDelivered
        '
        Me.CheckBoxDelivered.AutoSize = True
        Me.CheckBoxDelivered.Checked = True
        Me.CheckBoxDelivered.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxDelivered.Location = New System.Drawing.Point(12, 35)
        Me.CheckBoxDelivered.Name = "CheckBoxDelivered"
        Me.CheckBoxDelivered.Size = New System.Drawing.Size(70, 17)
        Me.CheckBoxDelivered.TabIndex = 1
        Me.CheckBoxDelivered.Text = "Enviades"
        Me.CheckBoxDelivered.UseVisualStyleBackColor = True
        '
        'CheckBoxDeliverPending
        '
        Me.CheckBoxDeliverPending.AutoSize = True
        Me.CheckBoxDeliverPending.Checked = True
        Me.CheckBoxDeliverPending.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxDeliverPending.Location = New System.Drawing.Point(12, 12)
        Me.CheckBoxDeliverPending.Name = "CheckBoxDeliverPending"
        Me.CheckBoxDeliverPending.Size = New System.Drawing.Size(111, 17)
        Me.CheckBoxDeliverPending.TabIndex = 2
        Me.CheckBoxDeliverPending.Text = "Pendents d'enviar"
        Me.CheckBoxDeliverPending.UseVisualStyleBackColor = True
        '
        'Frm_Customer_Invoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 261)
        Me.Controls.Add(Me.CheckBoxDeliverPending)
        Me.Controls.Add(Me.CheckBoxDelivered)
        Me.Controls.Add(Me.Xl_Invoices1)
        Me.Name = "Frm_Customer_Invoices"
        Me.Text = "Factures de ..."
        CType(Me.Xl_Invoices1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_Invoices1 As Xl_Invoices
    Friend WithEvents CheckBoxDelivered As CheckBox
    Friend WithEvents CheckBoxDeliverPending As CheckBox
End Class
