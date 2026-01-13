Public Class Xl_PrintDoc
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents RadioButtonPrint As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPreview As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBoxPrint As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxCopia As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxOriginal As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.RadioButtonPrint = New System.Windows.Forms.RadioButton
        Me.RadioButtonPreview = New System.Windows.Forms.RadioButton
        Me.GroupBoxPrint = New System.Windows.Forms.GroupBox
        Me.CheckBoxCopia = New System.Windows.Forms.CheckBox
        Me.CheckBoxOriginal = New System.Windows.Forms.CheckBox
        Me.GroupBoxPrint.SuspendLayout()
        Me.SuspendLayout()
        '
        'RadioButtonPrint
        '
        Me.RadioButtonPrint.Location = New System.Drawing.Point(0, 24)
        Me.RadioButtonPrint.Name = "RadioButtonPrint"
        Me.RadioButtonPrint.Size = New System.Drawing.Size(144, 24)
        Me.RadioButtonPrint.TabIndex = 7
        Me.RadioButtonPrint.Text = "Imprimeix"
        '
        'RadioButtonPreview
        '
        Me.RadioButtonPreview.Checked = True
        Me.RadioButtonPreview.Location = New System.Drawing.Point(0, 0)
        Me.RadioButtonPreview.Name = "RadioButtonPreview"
        Me.RadioButtonPreview.Size = New System.Drawing.Size(144, 24)
        Me.RadioButtonPreview.TabIndex = 6
        Me.RadioButtonPreview.TabStop = True
        Me.RadioButtonPreview.Text = "Vista previa"
        '
        'GroupBoxPrint
        '
        Me.GroupBoxPrint.Controls.Add(Me.CheckBoxCopia)
        Me.GroupBoxPrint.Controls.Add(Me.CheckBoxOriginal)
        Me.GroupBoxPrint.Location = New System.Drawing.Point(8, 32)
        Me.GroupBoxPrint.Name = "GroupBoxPrint"
        Me.GroupBoxPrint.Size = New System.Drawing.Size(208, 80)
        Me.GroupBoxPrint.TabIndex = 8
        Me.GroupBoxPrint.TabStop = False
        Me.GroupBoxPrint.Visible = False
        '
        'CheckBoxCopia
        '
        Me.CheckBoxCopia.Checked = True
        Me.CheckBoxCopia.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxCopia.Location = New System.Drawing.Point(24, 48)
        Me.CheckBoxCopia.Name = "CheckBoxCopia"
        Me.CheckBoxCopia.Size = New System.Drawing.Size(120, 16)
        Me.CheckBoxCopia.TabIndex = 1
        Me.CheckBoxCopia.Text = "Copia"
        '
        'CheckBoxOriginal
        '
        Me.CheckBoxOriginal.Location = New System.Drawing.Point(24, 24)
        Me.CheckBoxOriginal.Name = "CheckBoxOriginal"
        Me.CheckBoxOriginal.Size = New System.Drawing.Size(120, 16)
        Me.CheckBoxOriginal.TabIndex = 0
        Me.CheckBoxOriginal.Text = "Original"
        '
        'Xl_PrintDoc
        '
        Me.Controls.Add(Me.RadioButtonPrint)
        Me.Controls.Add(Me.RadioButtonPreview)
        Me.Controls.Add(Me.GroupBoxPrint)
        Me.Name = "Xl_PrintDoc"
        Me.Size = New System.Drawing.Size(216, 112)
        Me.GroupBoxPrint.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public ReadOnly Property Copia() As Boolean
        Get
            Return CheckBoxCopia.Checked
        End Get
    End Property

    Public ReadOnly Property Original() As Boolean
        Get
            Return CheckBoxOriginal.Checked
        End Get
    End Property

    Public ReadOnly Property Preview() As Boolean
        Get
            Return RadioButtonPreview.Checked
        End Get
    End Property

    Private Sub PrintVisible(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 RadioButtonPreview.Click, _
  RadioButtonPrint.Click
        GroupBoxPrint.Visible = RadioButtonPrint.Checked
    End Sub
End Class
