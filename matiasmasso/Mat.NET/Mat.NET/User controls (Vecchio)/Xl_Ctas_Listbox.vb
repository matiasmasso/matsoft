

Public Class Xl_Ctas_Listbox
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
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(150, 147)
        Me.ListBox1.TabIndex = 0
        '
        'Xl_Ctas_Listbox
        '
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "Xl_Ctas_Listbox"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdate()

    Private mCtas As PgcCtas

    Public Property Ctas() As PgcCtas
        Get
            Return mCtas
        End Get
        Set(ByVal Value As PgcCtas)
            mCtas = Value
            If Not mCtas Is Nothing Then
                ListBox1.Items.Clear()
                Dim oCta As PgcCta
                For Each oCta In mCtas
                    ListBox1.Items.Add(oCta.FullNom)
                Next
            End If
        End Set
    End Property

    Public Sub Add(ByVal oCta As PgcCta)
        Dim i As Integer
        For i = 0 To mCtas.Count - 1
            Select Case ListBox1.Items(i).ToString
                Case Is = oCta.FullNom
                    Exit Sub
                Case Is > oCta.FullNom
                    mCtas.Add(oCta)
                    Me.Ctas = mCtas
                    RaiseEvent AfterUpdate()
                    Exit Sub
            End Select
        Next
        mCtas.Add(oCta)
        Me.Ctas = mCtas
        RaiseEvent AfterUpdate()
    End Sub

    Public Sub RemoveCurrentCta()
        mCtas.RemoveAt(ListBox1.SelectedIndex)
        Me.Ctas = mCtas
        RaiseEvent AfterUpdate()
    End Sub
End Class
