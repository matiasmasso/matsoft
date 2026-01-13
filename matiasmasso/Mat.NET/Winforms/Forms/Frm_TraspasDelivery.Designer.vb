<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TraspasDelivery
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
        Me.Xl_TraspasDeliveryItems1 = New Winforms.Xl_TraspasDeliveryItems()
        Me.TextBoxHeader = New System.Windows.Forms.TextBox()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        CType(Me.Xl_TraspasDeliveryItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_TraspasDeliveryItems1
        '
        Me.Xl_TraspasDeliveryItems1.AllowUserToAddRows = False
        Me.Xl_TraspasDeliveryItems1.AllowUserToDeleteRows = False
        Me.Xl_TraspasDeliveryItems1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TraspasDeliveryItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_TraspasDeliveryItems1.DisplayObsolets = False
        Me.Xl_TraspasDeliveryItems1.Filter = Nothing
        Me.Xl_TraspasDeliveryItems1.Location = New System.Drawing.Point(1, 140)
        Me.Xl_TraspasDeliveryItems1.Name = "Xl_TraspasDeliveryItems1"
        Me.Xl_TraspasDeliveryItems1.ReadOnly = True
        Me.Xl_TraspasDeliveryItems1.Size = New System.Drawing.Size(398, 243)
        Me.Xl_TraspasDeliveryItems1.TabIndex = 0
        '
        'TextBoxHeader
        '
        Me.TextBoxHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxHeader.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxHeader.Location = New System.Drawing.Point(12, 13)
        Me.TextBoxHeader.Multiline = True
        Me.TextBoxHeader.Name = "TextBoxHeader"
        Me.TextBoxHeader.ReadOnly = True
        Me.TextBoxHeader.Size = New System.Drawing.Size(376, 92)
        Me.TextBoxHeader.TabIndex = 1
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(163, 114)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(235, 20)
        Me.Xl_TextboxSearch1.TabIndex = 2
        '
        'Frm_TraspasDelivery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 383)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.TextBoxHeader)
        Me.Controls.Add(Me.Xl_TraspasDeliveryItems1)
        Me.Name = "Frm_TraspasDelivery"
        Me.Text = "Albará de traspás"
        CType(Me.Xl_TraspasDeliveryItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_TraspasDeliveryItems1 As Xl_TraspasDeliveryItems
    Friend WithEvents TextBoxHeader As TextBox
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
