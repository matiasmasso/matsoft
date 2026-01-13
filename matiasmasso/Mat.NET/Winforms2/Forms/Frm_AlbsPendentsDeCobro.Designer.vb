<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AlbsPendentsDeCobro
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
        Me.Xl_Deliveries1 = New Mat.Net.Xl_Deliveries()
        Me.ComboBoxRetencioCod = New System.Windows.Forms.ComboBox()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        CType(Me.Xl_Deliveries1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Deliveries1
        '
        Me.Xl_Deliveries1.AllowUserToAddRows = False
        Me.Xl_Deliveries1.AllowUserToDeleteRows = False
        Me.Xl_Deliveries1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Deliveries1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Deliveries1.DisplayObsolets = False
        Me.Xl_Deliveries1.Filter = Nothing
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_Deliveries1, "Frm_AlbsPendentsDeCobro.htm#Xl_Deliveries1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_Deliveries1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_Deliveries1.Location = New System.Drawing.Point(0, 41)
        Me.Xl_Deliveries1.MouseIsDown = False
        Me.Xl_Deliveries1.Name = "Xl_Deliveries1"
        Me.Xl_Deliveries1.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.Xl_Deliveries1, True)
        Me.Xl_Deliveries1.Size = New System.Drawing.Size(629, 272)
        Me.Xl_Deliveries1.TabIndex = 0
        '
        'ComboBoxRetencioCod
        '
        Me.ComboBoxRetencioCod.FormattingEnabled = True
        Me.HelpProviderHG.SetHelpKeyword(Me.ComboBoxRetencioCod, "Frm_AlbsPendentsDeCobro.htm#ComboBoxRetencioCod")
        Me.HelpProviderHG.SetHelpNavigator(Me.ComboBoxRetencioCod, System.Windows.Forms.HelpNavigator.Topic)
        Me.ComboBoxRetencioCod.Items.AddRange(New Object() {"(triar motiu de retenció)", "Pendents per altres conceptes", "Pendents de Transferència", "Pendents de Visa"})
        Me.ComboBoxRetencioCod.Location = New System.Drawing.Point(385, 9)
        Me.ComboBoxRetencioCod.Name = "ComboBoxRetencioCod"
        Me.HelpProviderHG.SetShowHelp(Me.ComboBoxRetencioCod, True)
        Me.ComboBoxRetencioCod.Size = New System.Drawing.Size(244, 21)
        Me.ComboBoxRetencioCod.TabIndex = 1
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_AlbsPendentsDeCobro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 313)
        Me.Controls.Add(Me.ComboBoxRetencioCod)
        Me.Controls.Add(Me.Xl_Deliveries1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_AlbsPendentsDeCobro.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_AlbsPendentsDeCobro"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Albarans retinguts pendents de cobrament"
        CType(Me.Xl_Deliveries1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Deliveries1 As Xl_Deliveries
    Friend WithEvents ComboBoxRetencioCod As ComboBox
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
