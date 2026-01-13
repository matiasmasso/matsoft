<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Forecast_Old
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageFcast = New System.Windows.Forms.TabPage
        Me.CheckBoxHideLastInProduction = New System.Windows.Forms.CheckBox
        Me.CheckBoxHideSales = New System.Windows.Forms.CheckBox
        Me.CheckBoxHideObsoletos = New System.Windows.Forms.CheckBox
        Me.DataGridViewFcast = New System.Windows.Forms.DataGridView
        Me.ComboBoxYeas = New System.Windows.Forms.ComboBox
        Me.ToolStripFcast = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonFCastRefresca = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton
        Me.TabPagePro = New System.Windows.Forms.TabPage
        Me.ToolStripProposta = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonProNewPdc = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonExcelPrevisioCompres = New System.Windows.Forms.ToolStripButton
        Me.NumericUpDownM3 = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.DateTimePickerNextOrder = New System.Windows.Forms.DateTimePicker
        Me.NumericUpDownDelivery = New System.Windows.Forms.NumericUpDown
        Me.NumericUpDownNextOrder = New System.Windows.Forms.NumericUpDown
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DateTimePickerDeliver = New System.Windows.Forms.DateTimePicker
        Me.DataGridViewPro = New System.Windows.Forms.DataGridView
        Me.TabPageControl = New System.Windows.Forms.TabPage
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.DataGridViewControl = New System.Windows.Forms.DataGridView
        Me.CheckBoxSkipForecast = New System.Windows.Forms.CheckBox
        Me.TabControl1.SuspendLayout()
        Me.TabPageFcast.SuspendLayout()
        CType(Me.DataGridViewFcast, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripFcast.SuspendLayout()
        Me.TabPagePro.SuspendLayout()
        Me.ToolStripProposta.SuspendLayout()
        CType(Me.NumericUpDownM3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownNextOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewPro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageControl.SuspendLayout()
        CType(Me.DataGridViewControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageFcast)
        Me.TabControl1.Controls.Add(Me.TabPagePro)
        Me.TabControl1.Controls.Add(Me.TabPageControl)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(817, 435)
        Me.TabControl1.TabIndex = 7
        '
        'TabPageFcast
        '
        Me.TabPageFcast.Controls.Add(Me.CheckBoxHideLastInProduction)
        Me.TabPageFcast.Controls.Add(Me.CheckBoxHideSales)
        Me.TabPageFcast.Controls.Add(Me.CheckBoxHideObsoletos)
        Me.TabPageFcast.Controls.Add(Me.DataGridViewFcast)
        Me.TabPageFcast.Controls.Add(Me.ComboBoxYeas)
        Me.TabPageFcast.Controls.Add(Me.ToolStripFcast)
        Me.TabPageFcast.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFcast.Name = "TabPageFcast"
        Me.TabPageFcast.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageFcast.Size = New System.Drawing.Size(809, 409)
        Me.TabPageFcast.TabIndex = 0
        Me.TabPageFcast.Text = "DADES"
        Me.TabPageFcast.UseVisualStyleBackColor = True
        '
        'CheckBoxHideLastInProduction
        '
        Me.CheckBoxHideLastInProduction.AutoSize = True
        Me.CheckBoxHideLastInProduction.Location = New System.Drawing.Point(296, 9)
        Me.CheckBoxHideLastInProduction.Name = "CheckBoxHideLastInProduction"
        Me.CheckBoxHideLastInProduction.Size = New System.Drawing.Size(194, 17)
        Me.CheckBoxHideLastInProduction.TabIndex = 11
        Me.CheckBoxHideLastInProduction.Text = "Ocultar ultimes unitats en produccio"
        Me.CheckBoxHideLastInProduction.UseVisualStyleBackColor = True
        '
        'CheckBoxHideSales
        '
        Me.CheckBoxHideSales.AutoSize = True
        Me.CheckBoxHideSales.Location = New System.Drawing.Point(188, 9)
        Me.CheckBoxHideSales.Name = "CheckBoxHideSales"
        Me.CheckBoxHideSales.Size = New System.Drawing.Size(102, 17)
        Me.CheckBoxHideSales.TabIndex = 10
        Me.CheckBoxHideSales.Text = "Ocultar resultats"
        Me.CheckBoxHideSales.UseVisualStyleBackColor = True
        '
        'CheckBoxHideObsoletos
        '
        Me.CheckBoxHideObsoletos.AutoSize = True
        Me.CheckBoxHideObsoletos.Checked = True
        Me.CheckBoxHideObsoletos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHideObsoletos.Location = New System.Drawing.Point(504, 11)
        Me.CheckBoxHideObsoletos.Name = "CheckBoxHideObsoletos"
        Me.CheckBoxHideObsoletos.Size = New System.Drawing.Size(153, 17)
        Me.CheckBoxHideObsoletos.TabIndex = 9
        Me.CheckBoxHideObsoletos.Text = "Ocultar obsoletos sin datos"
        Me.CheckBoxHideObsoletos.UseVisualStyleBackColor = True
        '
        'DataGridViewFcast
        '
        Me.DataGridViewFcast.AllowUserToAddRows = False
        Me.DataGridViewFcast.AllowUserToDeleteRows = False
        Me.DataGridViewFcast.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewFcast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewFcast.Location = New System.Drawing.Point(3, 28)
        Me.DataGridViewFcast.Name = "DataGridViewFcast"
        Me.DataGridViewFcast.ReadOnly = True
        Me.DataGridViewFcast.Size = New System.Drawing.Size(803, 378)
        Me.DataGridViewFcast.TabIndex = 8
        '
        'ComboBoxYeas
        '
        Me.ComboBoxYeas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxYeas.FormattingEnabled = True
        Me.ComboBoxYeas.Location = New System.Drawing.Point(712, 7)
        Me.ComboBoxYeas.Name = "ComboBoxYeas"
        Me.ComboBoxYeas.Size = New System.Drawing.Size(91, 21)
        Me.ComboBoxYeas.TabIndex = 7
        '
        'ToolStripFcast
        '
        Me.ToolStripFcast.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonFCastRefresca, Me.ToolStripButtonExcel})
        Me.ToolStripFcast.Location = New System.Drawing.Point(3, 3)
        Me.ToolStripFcast.Name = "ToolStripFcast"
        Me.ToolStripFcast.Size = New System.Drawing.Size(803, 25)
        Me.ToolStripFcast.TabIndex = 5
        Me.ToolStripFcast.Text = "ToolStrip1"
        '
        'ToolStripButtonFCastRefresca
        '
        Me.ToolStripButtonFCastRefresca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFCastRefresca.Image = My.Resources.Resources.refresca
        Me.ToolStripButtonFCastRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFCastRefresca.Name = "ToolStripButtonFCastRefresca"
        Me.ToolStripButtonFCastRefresca.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonFCastRefresca.Text = "ToolStripButton1"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "Excel"
        Me.ToolStripButtonExcel.ToolTipText = "Exportar a Excel"
        '
        'TabPagePro
        '
        Me.TabPagePro.Controls.Add(Me.CheckBoxSkipForecast)
        Me.TabPagePro.Controls.Add(Me.ToolStripProposta)
        Me.TabPagePro.Controls.Add(Me.NumericUpDownM3)
        Me.TabPagePro.Controls.Add(Me.Label3)
        Me.TabPagePro.Controls.Add(Me.DateTimePickerNextOrder)
        Me.TabPagePro.Controls.Add(Me.NumericUpDownDelivery)
        Me.TabPagePro.Controls.Add(Me.NumericUpDownNextOrder)
        Me.TabPagePro.Controls.Add(Me.Label2)
        Me.TabPagePro.Controls.Add(Me.Label1)
        Me.TabPagePro.Controls.Add(Me.DateTimePickerDeliver)
        Me.TabPagePro.Controls.Add(Me.DataGridViewPro)
        Me.TabPagePro.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePro.Name = "TabPagePro"
        Me.TabPagePro.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePro.Size = New System.Drawing.Size(809, 409)
        Me.TabPagePro.TabIndex = 1
        Me.TabPagePro.Text = "PROPOSTA"
        Me.TabPagePro.UseVisualStyleBackColor = True
        '
        'ToolStripProposta
        '
        Me.ToolStripProposta.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonProNewPdc, Me.ToolStripButtonExcelPrevisioCompres})
        Me.ToolStripProposta.Location = New System.Drawing.Point(3, 3)
        Me.ToolStripProposta.Name = "ToolStripProposta"
        Me.ToolStripProposta.Size = New System.Drawing.Size(803, 25)
        Me.ToolStripProposta.TabIndex = 17
        Me.ToolStripProposta.Text = "ToolStrip2"
        '
        'ToolStripButtonProNewPdc
        '
        Me.ToolStripButtonProNewPdc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonProNewPdc.Image = My.Resources.Resources.NewDoc
        Me.ToolStripButtonProNewPdc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonProNewPdc.Name = "ToolStripButtonProNewPdc"
        Me.ToolStripButtonProNewPdc.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonProNewPdc.Text = "confeccionar proposta de comanda"
        '
        'ToolStripButtonExcelPrevisioCompres
        '
        Me.ToolStripButtonExcelPrevisioCompres.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcelPrevisioCompres.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcelPrevisioCompres.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcelPrevisioCompres.Name = "ToolStripButtonExcelPrevisioCompres"
        Me.ToolStripButtonExcelPrevisioCompres.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcelPrevisioCompres.Text = "Previsio de compres"
        Me.ToolStripButtonExcelPrevisioCompres.ToolTipText = "Previsio de Compres"
        '
        'NumericUpDownM3
        '
        Me.NumericUpDownM3.Location = New System.Drawing.Point(273, 42)
        Me.NumericUpDownM3.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDownM3.Name = "NumericUpDownM3"
        Me.NumericUpDownM3.Size = New System.Drawing.Size(48, 20)
        Me.NumericUpDownM3.TabIndex = 16
        Me.NumericUpDownM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDownM3.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(324, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "m3:"
        '
        'DateTimePickerNextOrder
        '
        Me.DateTimePickerNextOrder.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerNextOrder.Location = New System.Drawing.Point(560, 41)
        Me.DateTimePickerNextOrder.Name = "DateTimePickerNextOrder"
        Me.DateTimePickerNextOrder.Size = New System.Drawing.Size(89, 20)
        Me.DateTimePickerNextOrder.TabIndex = 14
        '
        'NumericUpDownDelivery
        '
        Me.NumericUpDownDelivery.Location = New System.Drawing.Point(124, 42)
        Me.NumericUpDownDelivery.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDownDelivery.Name = "NumericUpDownDelivery"
        Me.NumericUpDownDelivery.Size = New System.Drawing.Size(48, 20)
        Me.NumericUpDownDelivery.TabIndex = 13
        Me.NumericUpDownDelivery.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownDelivery.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'NumericUpDownNextOrder
        '
        Me.NumericUpDownNextOrder.Location = New System.Drawing.Point(506, 42)
        Me.NumericUpDownNextOrder.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDownNextOrder.Name = "NumericUpDownNextOrder"
        Me.NumericUpDownNextOrder.Size = New System.Drawing.Size(48, 20)
        Me.NumericUpDownNextOrder.TabIndex = 12
        Me.NumericUpDownNextOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownNextOrder.Value = New Decimal(New Integer() {7, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(365, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Dies fins següent comanda"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Plaç de entrega. Dies"
        '
        'DateTimePickerDeliver
        '
        Me.DateTimePickerDeliver.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDeliver.Location = New System.Drawing.Point(178, 42)
        Me.DateTimePickerDeliver.Name = "DateTimePickerDeliver"
        Me.DateTimePickerDeliver.Size = New System.Drawing.Size(89, 20)
        Me.DateTimePickerDeliver.TabIndex = 8
        '
        'DataGridViewPro
        '
        Me.DataGridViewPro.AllowUserToAddRows = False
        Me.DataGridViewPro.AllowUserToDeleteRows = False
        Me.DataGridViewPro.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewPro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewPro.Location = New System.Drawing.Point(3, 71)
        Me.DataGridViewPro.Name = "DataGridViewPro"
        Me.DataGridViewPro.ReadOnly = True
        Me.DataGridViewPro.Size = New System.Drawing.Size(798, 330)
        Me.DataGridViewPro.TabIndex = 7
        '
        'TabPageControl
        '
        Me.TabPageControl.Controls.Add(Me.ToolStrip1)
        Me.TabPageControl.Controls.Add(Me.DataGridViewControl)
        Me.TabPageControl.Location = New System.Drawing.Point(4, 22)
        Me.TabPageControl.Name = "TabPageControl"
        Me.TabPageControl.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageControl.Size = New System.Drawing.Size(809, 409)
        Me.TabPageControl.TabIndex = 2
        Me.TabPageControl.Text = "CONTROL"
        Me.TabPageControl.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(803, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'DataGridViewControl
        '
        Me.DataGridViewControl.AllowUserToAddRows = False
        Me.DataGridViewControl.AllowUserToDeleteRows = False
        Me.DataGridViewControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewControl.Location = New System.Drawing.Point(0, 31)
        Me.DataGridViewControl.Name = "DataGridViewControl"
        Me.DataGridViewControl.ReadOnly = True
        Me.DataGridViewControl.Size = New System.Drawing.Size(803, 378)
        Me.DataGridViewControl.TabIndex = 0
        '
        'CheckBoxSkipForecast
        '
        Me.CheckBoxSkipForecast.AutoSize = True
        Me.CheckBoxSkipForecast.Location = New System.Drawing.Point(124, 6)
        Me.CheckBoxSkipForecast.Name = "CheckBoxSkipForecast"
        Me.CheckBoxSkipForecast.Size = New System.Drawing.Size(108, 17)
        Me.CheckBoxSkipForecast.TabIndex = 18
        Me.CheckBoxSkipForecast.Text = "ignorar previsions"
        Me.CheckBoxSkipForecast.UseVisualStyleBackColor = True
        '
        'Frm_Forecast
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 435)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Forecast"
        Me.Text = "FORECAST"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageFcast.ResumeLayout(False)
        Me.TabPageFcast.PerformLayout()
        CType(Me.DataGridViewFcast, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripFcast.ResumeLayout(False)
        Me.ToolStripFcast.PerformLayout()
        Me.TabPagePro.ResumeLayout(False)
        Me.TabPagePro.PerformLayout()
        Me.ToolStripProposta.ResumeLayout(False)
        Me.ToolStripProposta.PerformLayout()
        CType(Me.NumericUpDownM3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownDelivery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownNextOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewPro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageControl.ResumeLayout(False)
        Me.TabPageControl.PerformLayout()
        CType(Me.DataGridViewControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageFcast As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewFcast As System.Windows.Forms.DataGridView
    Friend WithEvents ComboBoxYeas As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripFcast As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabPagePro As System.Windows.Forms.TabPage
    Friend WithEvents DateTimePickerDeliver As System.Windows.Forms.DateTimePicker
    Friend WithEvents DataGridViewPro As System.Windows.Forms.DataGridView
    Friend WithEvents NumericUpDownDelivery As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownNextOrder As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerNextOrder As System.Windows.Forms.DateTimePicker
    Friend WithEvents TabPageControl As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewControl As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents CheckBoxHideObsoletos As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDownM3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolStripButtonFCastRefresca As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripProposta As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonProNewPdc As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonExcelPrevisioCompres As System.Windows.Forms.ToolStripButton
    Friend WithEvents CheckBoxHideSales As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxHideLastInProduction As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSkipForecast As System.Windows.Forms.CheckBox
End Class
