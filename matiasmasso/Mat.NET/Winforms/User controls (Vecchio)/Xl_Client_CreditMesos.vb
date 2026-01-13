Public Class Xl_Client_CreditMesos
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
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.Items.AddRange(New Object() {"a la vista", "30 dias", "60 dias", "90 dias"})
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(80, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'Xl_Client_CreditMesos
        '
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "Xl_Client_CreditMesos"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Event AfterUpdate(ByVal IntMesos As Integer)

    Private mAllowEvents As Boolean

    Public Property Mesos() As Integer
        Get
            Return ComboBox1.SelectedIndex
        End Get
        Set(ByVal Value As Integer)
            ComboBox1.SelectedIndex = Value
            mAllowEvents = True
        End Set
    End Property

    Private Sub Xl_Client_CreditMesos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        MyBase.Height = ComboBox1.Height
        ComboBox1.Width = MyBase.Width
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If mAllowEvents Then
            RaiseEvent AfterUpdate(ComboBox1.SelectedIndex)
        End If
    End Sub
End Class
