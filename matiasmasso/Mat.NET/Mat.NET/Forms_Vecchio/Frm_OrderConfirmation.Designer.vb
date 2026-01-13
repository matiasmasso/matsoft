<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OrderConfirmation
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxOrderConfirmationNum = New System.Windows.Forms.TextBox()
        Me.TextBoxPdc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Xl_ContactSupplier = New Mat.NET.Xl_Contact()
        Me.Xl_ContactBuyer = New Mat.NET.Xl_Contact()
        Me.Xl_ContactDelivery = New Mat.NET.Xl_Contact()
        Me.Xl_ContactInvoice = New Mat.NET.Xl_Contact()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ButtonBrowsePdc = New System.Windows.Forms.Button()
        Me.Xl_AmtCur1 = New Mat.Net.Xl_AmountCur()
        Me.Xl_OrderConfirmationItems1 = New Mat.NET.Xl_OrderConfirmationItems()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 420)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(702, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(483, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel·lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(594, 4)
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
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(63, 19)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(129, 20)
        Me.DateTimePicker1.TabIndex = 44
        '
        'TextBoxOrderConfirmationNum
        '
        Me.TextBoxOrderConfirmationNum.Location = New System.Drawing.Point(63, 45)
        Me.TextBoxOrderConfirmationNum.Name = "TextBoxOrderConfirmationNum"
        Me.TextBoxOrderConfirmationNum.Size = New System.Drawing.Size(129, 20)
        Me.TextBoxOrderConfirmationNum.TabIndex = 45
        '
        'TextBoxPdc
        '
        Me.TextBoxPdc.Location = New System.Drawing.Point(63, 71)
        Me.TextBoxPdc.Name = "TextBoxPdc"
        Me.TextBoxPdc.ReadOnly = True
        Me.TextBoxPdc.Size = New System.Drawing.Size(99, 20)
        Me.TextBoxPdc.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Num."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Comanda"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Proveidor"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Comprador"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Entrega"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Factura"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(5, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 13)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "Data"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 13)
        Me.Label8.TabIndex = 60
        Me.Label8.Text = "Import"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Xl_ContactSupplier)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Xl_ContactBuyer)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Xl_ContactDelivery)
        Me.GroupBox1.Controls.Add(Me.Xl_ContactInvoice)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(480, 138)
        Me.GroupBox1.TabIndex = 61
        Me.GroupBox1.TabStop = False
        '
        'Xl_ContactSupplier
        '
        Me.Xl_ContactSupplier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactSupplier.Contact = Nothing
        Me.Xl_ContactSupplier.Location = New System.Drawing.Point(64, 19)
        Me.Xl_ContactSupplier.Name = "Xl_ContactSupplier"
        Me.Xl_ContactSupplier.ReadOnly = False
        Me.Xl_ContactSupplier.Size = New System.Drawing.Size(399, 20)
        Me.Xl_ContactSupplier.TabIndex = 42
        '
        'Xl_ContactBuyer
        '
        Me.Xl_ContactBuyer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactBuyer.Contact = Nothing
        Me.Xl_ContactBuyer.Location = New System.Drawing.Point(64, 45)
        Me.Xl_ContactBuyer.Name = "Xl_ContactBuyer"
        Me.Xl_ContactBuyer.ReadOnly = False
        Me.Xl_ContactBuyer.Size = New System.Drawing.Size(399, 20)
        Me.Xl_ContactBuyer.TabIndex = 52
        '
        'Xl_ContactDelivery
        '
        Me.Xl_ContactDelivery.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactDelivery.Contact = Nothing
        Me.Xl_ContactDelivery.Location = New System.Drawing.Point(64, 71)
        Me.Xl_ContactDelivery.Name = "Xl_ContactDelivery"
        Me.Xl_ContactDelivery.ReadOnly = False
        Me.Xl_ContactDelivery.Size = New System.Drawing.Size(399, 20)
        Me.Xl_ContactDelivery.TabIndex = 54
        '
        'Xl_ContactInvoice
        '
        Me.Xl_ContactInvoice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactInvoice.Contact = Nothing
        Me.Xl_ContactInvoice.Location = New System.Drawing.Point(64, 97)
        Me.Xl_ContactInvoice.Name = "Xl_ContactInvoice"
        Me.Xl_ContactInvoice.ReadOnly = False
        Me.Xl_ContactInvoice.Size = New System.Drawing.Size(399, 20)
        Me.Xl_ContactInvoice.TabIndex = 56
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.ButtonBrowsePdc)
        Me.GroupBox2.Controls.Add(Me.Xl_AmtCur1)
        Me.GroupBox2.Controls.Add(Me.TextBoxOrderConfirmationNum)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.TextBoxPdc)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(498, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 138)
        Me.GroupBox2.TabIndex = 62
        Me.GroupBox2.TabStop = False
        '
        'ButtonBrowsePdc
        '
        Me.ButtonBrowsePdc.Location = New System.Drawing.Point(160, 71)
        Me.ButtonBrowsePdc.Name = "ButtonBrowsePdc"
        Me.ButtonBrowsePdc.Size = New System.Drawing.Size(32, 20)
        Me.ButtonBrowsePdc.TabIndex = 62
        Me.ButtonBrowsePdc.Text = "..."
        Me.ButtonBrowsePdc.UseVisualStyleBackColor = True
        '
        'Xl_AmtCur1
        '
        Me.Xl_AmtCur1.Amt = Nothing
        Me.Xl_AmtCur1.Location = New System.Drawing.Point(63, 100)
        Me.Xl_AmtCur1.Name = "Xl_AmtCur1"
        Me.Xl_AmtCur1.Size = New System.Drawing.Size(129, 20)
        Me.Xl_AmtCur1.TabIndex = 61
        '
        'Xl_OrderConfirmationItems1
        '
        Me.Xl_OrderConfirmationItems1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_OrderConfirmationItems1.Location = New System.Drawing.Point(6, 157)
        Me.Xl_OrderConfirmationItems1.Name = "Xl_OrderConfirmationItems1"
        Me.Xl_OrderConfirmationItems1.Size = New System.Drawing.Size(692, 257)
        Me.Xl_OrderConfirmationItems1.TabIndex = 51
        '
        'Frm_OrderConfirmation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 451)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Xl_OrderConfirmationItems1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_OrderConfirmation"
        Me.Text = "Confirmació de comanda"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_ContactSupplier As Mat.NET.Xl_Contact
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxOrderConfirmationNum As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPdc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_OrderConfirmationItems1 As Mat.NET.Xl_OrderConfirmationItems
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactBuyer As Mat.NET.Xl_Contact
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactDelivery As Mat.NET.Xl_Contact
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactInvoice As Mat.NET.Xl_Contact
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonBrowsePdc As System.Windows.Forms.Button
    Friend WithEvents Xl_AmtCur1 As Mat.Net.Xl_AmountCur
End Class
