

Public Class Xl_Banc_Menu
    Inherits System.Windows.Forms.ContextMenu

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Private mBanc As MaxiSrvr.Banc

    Public WriteOnly Property Banc() As MaxiSrvr.Banc
        Set(ByVal Value As MaxiSrvr.Banc)
            mBanc = Value
            With MyBase.MenuItems
                Dim oItm As MenuItem
                Dim oStm As MenuItem
                .Clear()

                'directori
                'root.AddMenuContact(Me, mBanc)

                'remeses
                oItm = .Add("remeses")
                With oItm.MenuItems
                    .Add("Ultimes remeses", New System.EventHandler(AddressOf LastCsas))
                    oStm = .Add("remeses al cobro (Norma 19)")
                    With oStm.MenuItems
                        .Add("Nova remesa", New System.EventHandler(AddressOf NewAEB19))
                        .Add("Impago", New System.EventHandler(AddressOf AEB19Impago))
                    End With
                End With

                'pagaments
                oItm = .Add("pagaments")
                With oItm.MenuItems
                    .Add("rebuts", New System.EventHandler(AddressOf PayEfte))
                End With

                'extracte
                oItm = .Add("extracte", New System.EventHandler(AddressOf Extracte))
            End With
        End Set
    End Property

    Private Sub LastCsas(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowCsas(mBanc)
    End Sub

    Private Sub NewAEB19(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox("comando obsolet")
        'root.NewBancAEB19(mBanc)
    End Sub

    Private Sub AEB19Impago(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.NewBancAEB19Impag(mBanc)
    End Sub

    Private Sub Extracte(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oContact As Contact = mBanc
        Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.bancs)
        Dim oFrm As New Frm_CliCtas(oContact, oCta)
        oFrm.Show()
    End Sub

    Private Sub PayEfte(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
