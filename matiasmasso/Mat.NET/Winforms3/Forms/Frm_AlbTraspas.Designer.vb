<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AlbTraspas
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_Contact2From = New Mat.Net.Xl_Contact2()
        Me.Xl_Contact2To = New Mat.Net.Xl_Contact2()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Xl_DeliveryItems1 = New Mat.Net.Xl_DeliveryItems()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_DeliveryItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sortida de:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Entrada a:"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(101, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'Xl_Contact2From
        '
        Me.Xl_Contact2From.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2From.Contact = Nothing
        Me.Xl_Contact2From.Emp = Nothing
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_Contact2From, "Frm_AlbTraspas.htm#Xl_Contact2")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_Contact2From, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_Contact2From.Location = New System.Drawing.Point(88, 62)
        Me.Xl_Contact2From.Name = "Xl_Contact2From"
        Me.Xl_Contact2From.ReadOnly = False
        Me.HelpProviderHG.SetShowHelp(Me.Xl_Contact2From, True)
        Me.Xl_Contact2From.Size = New System.Drawing.Size(700, 20)
        Me.Xl_Contact2From.TabIndex = 3
        '
        'Xl_Contact2To
        '
        Me.Xl_Contact2To.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2To.Contact = Nothing
        Me.Xl_Contact2To.Emp = Nothing
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_Contact2To, "Frm_AlbTraspas.htm#Xl_Contact2")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_Contact2To, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_Contact2To.Location = New System.Drawing.Point(88, 88)
        Me.Xl_Contact2To.Name = "Xl_Contact2To"
        Me.Xl_Contact2To.ReadOnly = False
        Me.HelpProviderHG.SetShowHelp(Me.Xl_Contact2To, True)
        Me.Xl_Contact2To.Size = New System.Drawing.Size(700, 20)
        Me.Xl_Contact2To.TabIndex = 4
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.HelpProviderHG.SetHelpKeyword(Me.DateTimePicker1, "Frm_AlbTraspas.htm#DateTimePicker1")
        Me.HelpProviderHG.SetHelpNavigator(Me.DateTimePicker1, System.Windows.Forms.HelpNavigator.Topic)
        Me.DateTimePicker1.Location = New System.Drawing.Point(691, 36)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.HelpProviderHG.SetShowHelp(Me.DateTimePicker1, True)
        Me.DateTimePicker1.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker1.TabIndex = 5
        '
        'Xl_DeliveryItems1
        '
        Me.Xl_DeliveryItems1.AllowUserToAddRows = False
        Me.Xl_DeliveryItems1.AllowUserToDeleteRows = False
        Me.Xl_DeliveryItems1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DeliveryItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_DeliveryItems1, "Frm_AlbTraspas.htm#Xl_DeliveryItems1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_DeliveryItems1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_DeliveryItems1.Location = New System.Drawing.Point(26, 114)
        Me.Xl_DeliveryItems1.Name = "Xl_DeliveryItems1"
        Me.Xl_DeliveryItems1.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.Xl_DeliveryItems1, True)
        Me.Xl_DeliveryItems1.Size = New System.Drawing.Size(762, 324)
        Me.Xl_DeliveryItems1.TabIndex = 6
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_AlbTraspas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Xl_DeliveryItems1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Xl_Contact2To)
        Me.Controls.Add(Me.Xl_Contact2From)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_AlbTraspas.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_AlbTraspas"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Traspàs de magatzem"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_DeliveryItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_Contact2From As Xl_Contact2
    Friend WithEvents Xl_Contact2To As Xl_Contact2
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Xl_DeliveryItems1 As Xl_DeliveryItems
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
