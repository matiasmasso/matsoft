Public Class Xl_Yea
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
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Dock = System.Windows.Forms.DockStyle.Right
        Me.NumericUpDown1.Location = New System.Drawing.Point(33, 0)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {2099, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(16, 20)
        Me.NumericUpDown1.TabIndex = 1
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1985, 0, 0, 0})
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(33, 20)
        Me.TextBox1.TabIndex = 2
        '
        'Xl_Yea
        '
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Name = "Xl_Yea"
        Me.Size = New System.Drawing.Size(49, 20)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)
    Private mAllowEvents As Boolean

    Public Property Yea() As Integer
        Get
            If TextBox1.Text = "" Then
                Return 0
            Else
                Return TextBox1.Text
            End If
        End Get
        Set(ByVal Value As Integer)
            If Value.ToString.Length > 4 Then
                Value = CInt(Value.ToString.Substring(Value.ToString.Length - 4))
            End If
            TextBox1.Text = Value
            NumericUpDown1.Value = Value
            mAllowEvents = True
        End Set
    End Property


    Private Sub NumericUpDown1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDown1.Click
        'TextBox1.Text = NumericUpDown1.Validate
        TextBox1.Text = NumericUpDown1.Value
        RaiseEvent AfterUpdate(TextBox1.Text, EventArgs.Empty)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If IsNumeric(TextBox1.Text) Then
            If CInt(TextBox1.Text) >= NumericUpDown1.Minimum And CInt(TextBox1.Text) <= NumericUpDown1.Maximum Then
                NumericUpDown1.Value = TextBox1.Text
            End If
        End If
    End Sub
End Class
