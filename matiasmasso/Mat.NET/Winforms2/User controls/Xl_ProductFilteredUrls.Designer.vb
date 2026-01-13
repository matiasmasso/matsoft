<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_ProductFilteredUrls
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
        Me.components = New System.ComponentModel.Container()
        Me.Xl_CheckedFilters1 = New Xl_CheckedFilters()
        Me.ButtonCopyLink = New System.Windows.Forms.Button()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ButtonBrowse = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Xl_CheckedFilters1
        '
        Me.Xl_CheckedFilters1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CheckedFilters1.isDirty = False
        Me.Xl_CheckedFilters1.Location = New System.Drawing.Point(0, 3)
        Me.Xl_CheckedFilters1.Name = "Xl_CheckedFilters1"
        Me.Xl_CheckedFilters1.Size = New System.Drawing.Size(351, 166)
        Me.Xl_CheckedFilters1.TabIndex = 0
        '
        'ButtonCopyLink
        '
        Me.ButtonCopyLink.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCopyLink.Location = New System.Drawing.Point(206, 195)
        Me.ButtonCopyLink.Name = "ButtonCopyLink"
        Me.ButtonCopyLink.Size = New System.Drawing.Size(75, 22)
        Me.ButtonCopyLink.TabIndex = 1
        Me.ButtonCopyLink.Text = "Copiar"
        Me.ButtonCopyLink.UseVisualStyleBackColor = True
        '
        'TextBoxUrl
        '
        Me.TextBoxUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUrl.Location = New System.Drawing.Point(0, 196)
        Me.TextBoxUrl.Name = "TextBoxUrl"
        Me.TextBoxUrl.Size = New System.Drawing.Size(206, 20)
        Me.TextBoxUrl.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Location = New System.Drawing.Point(0, 175)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(351, 13)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = "Triar els filtres per generar la Url de la landing page corresponent"
        '
        'ButtonBrowse
        '
        Me.ButtonBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonBrowse.Location = New System.Drawing.Point(279, 195)
        Me.ButtonBrowse.Name = "ButtonBrowse"
        Me.ButtonBrowse.Size = New System.Drawing.Size(75, 22)
        Me.ButtonBrowse.TabIndex = 4
        Me.ButtonBrowse.Text = "Navegar"
        Me.ButtonBrowse.UseVisualStyleBackColor = True
        '
        'Xl_ProductFilteredUrls
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ButtonBrowse)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.ButtonCopyLink)
        Me.Controls.Add(Me.Xl_CheckedFilters1)
        Me.Controls.Add(Me.TextBoxUrl)
        Me.Name = "Xl_ProductFilteredUrls"
        Me.Size = New System.Drawing.Size(354, 217)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_CheckedFilters1 As Xl_CheckedFilters
    Friend WithEvents ButtonCopyLink As Button
    Friend WithEvents TextBoxUrl As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ButtonBrowse As Button
End Class
