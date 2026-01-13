

Public Class Xl_Cfp
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
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'Xl_Cfp
        '
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "Xl_Cfp"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mCod As MaxiSrvr.Cod
    Private mAllowEvents As Boolean

    Public Event AfterUpdate()

    Public Property Cod() As MaxiSrvr.Cod
        Get
            Return ComboBox1.SelectedItem
        End Get
        Set(ByVal Value As MaxiSrvr.Cod)
            If Not Value Is Nothing Then
                If ComboBox1.Items.Count = 0 Then LoadCfpCods()
                Dim i As Integer
                For i = 0 To ComboBox1.Items.Count - 1
                    If Value.Id = CType(ComboBox1.Items(i), Cod).Id Then
                        ComboBox1.SelectedIndex = i
                        Exit For
                    End If
                Next
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Sub LoadCfpCods()
        Dim oCodEpg As New CodEpg(10)
        Dim itm As MaxiSrvr.Cod
        With ComboBox1
            .ValueMember = "Id"
            .DisplayMember = "Nom"
            .Items.Add(New MaxiSrvr.Cod(0, ""))
            For Each itm In oCodEpg.Cods
                .Items.Add(itm)
            Next
        End With
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If mAllowEvents Then
            RaiseEvent AfterUpdate()
        End If
    End Sub

    Private Sub Xl_Cfp_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        MyBase.Height = ComboBox1.Height
        ComboBox1.Width = MyBase.Width
    End Sub

    Private Sub Xl_Cfp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ComboBox1.Items.Count = 0 Then LoadCfpCods()
    End Sub
End Class
