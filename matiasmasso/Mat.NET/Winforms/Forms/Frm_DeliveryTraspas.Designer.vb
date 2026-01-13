<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DeliveryTraspas
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
        Me.TextBoxId = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_ContactTo = New Winforms.Xl_Contact2()
        Me.Xl_ContactFrom = New Winforms.Xl_Contact2()
        Me.Xl_DeliveryTraspasItems1 = New Winforms.Xl_DeliveryTraspasItems()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_DeliveryTraspasItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(73, 22)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 59
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(73, 48)
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxId.TabIndex = 58
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 635)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(517, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(401, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Albarà"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Data"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "Origen"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 64
        Me.Label4.Text = "Destinació"
        '
        'Xl_ContactTo
        '
        Me.Xl_ContactTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactTo.Contact = Nothing
        Me.Xl_ContactTo.Emp = Nothing
        Me.Xl_ContactTo.Location = New System.Drawing.Point(73, 101)
        Me.Xl_ContactTo.Name = "Xl_ContactTo"
        Me.Xl_ContactTo.ReadOnly = False
        Me.Xl_ContactTo.Size = New System.Drawing.Size(440, 20)
        Me.Xl_ContactTo.TabIndex = 63
        '
        'Xl_ContactFrom
        '
        Me.Xl_ContactFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactFrom.Contact = Nothing
        Me.Xl_ContactFrom.Emp = Nothing
        Me.Xl_ContactFrom.Location = New System.Drawing.Point(73, 75)
        Me.Xl_ContactFrom.Name = "Xl_ContactFrom"
        Me.Xl_ContactFrom.ReadOnly = False
        Me.Xl_ContactFrom.Size = New System.Drawing.Size(440, 20)
        Me.Xl_ContactFrom.TabIndex = 61
        '
        'Xl_DeliveryTraspasItems1
        '
        Me.Xl_DeliveryTraspasItems1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DeliveryTraspasItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DeliveryTraspasItems1.DisplayObsolets = False
        Me.Xl_DeliveryTraspasItems1.Filter = Nothing
        Me.Xl_DeliveryTraspasItems1.Location = New System.Drawing.Point(0, 135)
        Me.Xl_DeliveryTraspasItems1.MouseIsDown = False
        Me.Xl_DeliveryTraspasItems1.Name = "Xl_DeliveryTraspasItems1"
        Me.Xl_DeliveryTraspasItems1.Size = New System.Drawing.Size(517, 497)
        Me.Xl_DeliveryTraspasItems1.TabIndex = 65
        '
        'Frm_DeliveryTraspas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 666)
        Me.Controls.Add(Me.Xl_DeliveryTraspasItems1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_ContactTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_ContactFrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxId)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_DeliveryTraspas"
        Me.Text = "Traspas de magatzem"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_DeliveryTraspasItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents TextBoxId As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_ContactFrom As Xl_Contact2
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_ContactTo As Xl_Contact2
    Friend WithEvents Xl_DeliveryTraspasItems1 As Xl_DeliveryTraspasItems
End Class
