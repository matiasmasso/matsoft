<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_StoreLocator
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_StoreLocatorDistributors1 = New Mat.Net.Xl_StoreLocatorDistributors()
        Me.ComboBoxLocation = New System.Windows.Forms.ComboBox()
        Me.ComboBoxZona = New System.Windows.Forms.ComboBox()
        Me.ComboBoxCountry = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.LabelCaption = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_StoreLocatorDistributors1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(653, 232)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LabelCaption)
        Me.TabPage1.Controls.Add(Me.Xl_StoreLocatorDistributors1)
        Me.TabPage1.Controls.Add(Me.ComboBoxLocation)
        Me.TabPage1.Controls.Add(Me.ComboBoxZona)
        Me.TabPage1.Controls.Add(Me.ComboBoxCountry)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(645, 206)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Botigues presencials"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_StoreLocatorDistributors1
        '
        Me.Xl_StoreLocatorDistributors1.AllowUserToAddRows = False
        Me.Xl_StoreLocatorDistributors1.AllowUserToDeleteRows = False
        Me.Xl_StoreLocatorDistributors1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_StoreLocatorDistributors1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_StoreLocatorDistributors1.DisplayObsolets = False
        Me.Xl_StoreLocatorDistributors1.Filter = Nothing
        Me.Xl_StoreLocatorDistributors1.Location = New System.Drawing.Point(8, 57)
        Me.Xl_StoreLocatorDistributors1.MouseIsDown = False
        Me.Xl_StoreLocatorDistributors1.Name = "Xl_StoreLocatorDistributors1"
        Me.Xl_StoreLocatorDistributors1.ReadOnly = True
        Me.Xl_StoreLocatorDistributors1.Size = New System.Drawing.Size(630, 147)
        Me.Xl_StoreLocatorDistributors1.TabIndex = 3
        '
        'ComboBoxLocation
        '
        Me.ComboBoxLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxLocation.FormattingEnabled = True
        Me.ComboBoxLocation.Location = New System.Drawing.Point(306, 29)
        Me.ComboBoxLocation.Name = "ComboBoxLocation"
        Me.ComboBoxLocation.Size = New System.Drawing.Size(333, 21)
        Me.ComboBoxLocation.TabIndex = 2
        '
        'ComboBoxZona
        '
        Me.ComboBoxZona.FormattingEnabled = True
        Me.ComboBoxZona.Location = New System.Drawing.Point(86, 29)
        Me.ComboBoxZona.Name = "ComboBoxZona"
        Me.ComboBoxZona.Size = New System.Drawing.Size(214, 21)
        Me.ComboBoxZona.TabIndex = 1
        '
        'ComboBoxCountry
        '
        Me.ComboBoxCountry.FormattingEnabled = True
        Me.ComboBoxCountry.Location = New System.Drawing.Point(8, 29)
        Me.ComboBoxCountry.Name = "ComboBoxCountry"
        Me.ComboBoxCountry.Size = New System.Drawing.Size(72, 21)
        Me.ComboBoxCountry.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(645, 206)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Comerços Online"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(1, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(653, 255)
        Me.Panel1.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 232)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(653, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'LabelCaption
        '
        Me.LabelCaption.AutoSize = True
        Me.LabelCaption.Location = New System.Drawing.Point(7, 10)
        Me.LabelCaption.Name = "LabelCaption"
        Me.LabelCaption.Size = New System.Drawing.Size(204, 13)
        Me.LabelCaption.TabIndex = 4
        Me.LabelCaption.Text = "Clients que han comprat alguna vegada..."
        '
        'Frm_StoreLocator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 286)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_StoreLocator"
        Me.Text = "Store Locator"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.Xl_StoreLocatorDistributors1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents ComboBoxLocation As ComboBox
    Friend WithEvents ComboBoxZona As ComboBox
    Friend WithEvents ComboBoxCountry As ComboBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_StoreLocatorDistributors1 As Xl_StoreLocatorDistributors
    Friend WithEvents LabelCaption As Label
End Class
