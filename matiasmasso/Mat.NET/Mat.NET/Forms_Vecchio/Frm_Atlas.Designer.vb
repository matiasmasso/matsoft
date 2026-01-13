Public Partial Class Frm_Atlas
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.components = New System.ComponentModel.Container()
        Me.ComboBoxPaisos = New System.Windows.Forms.ComboBox()
        Me.ComboBoxZonas = New System.Windows.Forms.ComboBox()
        Me.ComboBoxZips = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ComboBoxLocations = New System.Windows.Forms.ComboBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxPaisos
        '
        Me.ComboBoxPaisos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxPaisos.FormattingEnabled = True
        Me.ComboBoxPaisos.Location = New System.Drawing.Point(13, 13)
        Me.ComboBoxPaisos.Name = "ComboBoxPaisos"
        Me.ComboBoxPaisos.Size = New System.Drawing.Size(248, 21)
        Me.ComboBoxPaisos.TabIndex = 0
        '
        'ComboBoxZonas
        '
        Me.ComboBoxZonas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxZonas.FormattingEnabled = True
        Me.ComboBoxZonas.Location = New System.Drawing.Point(13, 31)
        Me.ComboBoxZonas.Name = "ComboBoxZonas"
        Me.ComboBoxZonas.Size = New System.Drawing.Size(366, 21)
        Me.ComboBoxZonas.TabIndex = 1
        '
        'ComboBoxZips
        '
        Me.ComboBoxZips.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxZips.FormattingEnabled = True
        Me.ComboBoxZips.Location = New System.Drawing.Point(13, 89)
        Me.ComboBoxZips.Name = "ComboBoxZips"
        Me.ComboBoxZips.Size = New System.Drawing.Size(366, 21)
        Me.ComboBoxZips.TabIndex = 2
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.AllowDrop = True
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(13, 153)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(366, 186)
        Me.DataGridView1.TabIndex = 3
        '
        'ComboBoxLocations
        '
        Me.ComboBoxLocations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxLocations.FormattingEnabled = True
        Me.ComboBoxLocations.Location = New System.Drawing.Point(12, 58)
        Me.ComboBoxLocations.Name = "ComboBoxLocations"
        Me.ComboBoxLocations.Size = New System.Drawing.Size(366, 21)
        Me.ComboBoxLocations.TabIndex = 4
        '
        'Frm_Atlas
        '
        Me.ClientSize = New System.Drawing.Size(391, 351)
        Me.Controls.Add(Me.ComboBoxLocations)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ComboBoxZips)
        Me.Controls.Add(Me.ComboBoxZonas)
        Me.Controls.Add(Me.ComboBoxPaisos)
        Me.Name = "Frm_Atlas"
        Me.Text = "ATLAS"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBoxPaisos As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxZonas As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxZips As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ComboBoxLocations As System.Windows.Forms.ComboBox
End Class
