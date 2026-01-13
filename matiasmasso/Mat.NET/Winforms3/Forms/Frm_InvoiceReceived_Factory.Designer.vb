<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_InvoiceReceived_Factory
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxDocNum = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_InvoiceReceivedItemsFactory1 = New Mat.Net.Xl_InvoiceReceivedItemsFactory()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_Contact21 = New Mat.Net.Xl_Contact2()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmountCurTotal = New Mat.Net.Xl_AmountCur()
        Me.Xl_Cur1 = New Mat.Net.Xl_Cur()
        Me.PanelButtons.SuspendLayout()
        CType(Me.Xl_InvoiceReceivedItemsFactory1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(380, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Factura nº"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(552, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Data"
        '
        'TextBoxDocNum
        '
        Me.TextBoxDocNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDocNum.Location = New System.Drawing.Point(442, 9)
        Me.TextBoxDocNum.Name = "TextBoxDocNum"
        Me.TextBoxDocNum.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxDocNum.TabIndex = 2
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(588, 9)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(98, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 328)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(688, 31)
        Me.PanelButtons.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(469, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(580, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 6
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_InvoiceReceivedItemsFactory1
        '
        Me.Xl_InvoiceReceivedItemsFactory1.AllowUserToAddRows = False
        Me.Xl_InvoiceReceivedItemsFactory1.AllowUserToDeleteRows = False
        Me.Xl_InvoiceReceivedItemsFactory1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_InvoiceReceivedItemsFactory1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InvoiceReceivedItemsFactory1.DisplayObsolets = False
        Me.Xl_InvoiceReceivedItemsFactory1.Location = New System.Drawing.Point(0, 35)
        Me.Xl_InvoiceReceivedItemsFactory1.MouseIsDown = False
        Me.Xl_InvoiceReceivedItemsFactory1.Name = "Xl_InvoiceReceivedItemsFactory1"
        Me.Xl_InvoiceReceivedItemsFactory1.ReadOnly = True
        Me.Xl_InvoiceReceivedItemsFactory1.Size = New System.Drawing.Size(688, 262)
        Me.Xl_InvoiceReceivedItemsFactory1.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(501, 306)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Total"
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Emp = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(60, 9)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(262, 20)
        Me.Xl_Contact21.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Proveidor"
        '
        'Xl_AmountCurTotal
        '
        Me.Xl_AmountCurTotal.Amt = Nothing
        Me.Xl_AmountCurTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmountCurTotal.Location = New System.Drawing.Point(535, 304)
        Me.Xl_AmountCurTotal.Name = "Xl_AmountCurTotal"
        Me.Xl_AmountCurTotal.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmountCurTotal.TabIndex = 5
        '
        'Xl_Cur1
        '
        Me.Xl_Cur1.Cur = Nothing
        Me.Xl_Cur1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Xl_Cur1.Location = New System.Drawing.Point(328, 9)
        Me.Xl_Cur1.Name = "Xl_Cur1"
        Me.Xl_Cur1.ReadOnly = True
        Me.Xl_Cur1.Size = New System.Drawing.Size(35, 20)
        Me.Xl_Cur1.TabIndex = 1
        Me.Xl_Cur1.TabStop = False
        '
        'Frm_InvoiceReceived_Factory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 359)
        Me.Controls.Add(Me.Xl_Cur1)
        Me.Controls.Add(Me.Xl_AmountCurTotal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_InvoiceReceivedItemsFactory1)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxDocNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_InvoiceReceived_Factory"
        Me.Text = "Nova factura de compres"
        Me.PanelButtons.ResumeLayout(False)
        CType(Me.Xl_InvoiceReceivedItemsFactory1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxDocNum As TextBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Xl_InvoiceReceivedItemsFactory1 As Xl_InvoiceReceivedItemsFactory
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_AmountCurTotal As Xl_AmountCur
    Friend WithEvents Xl_Cur1 As Xl_Cur
End Class
