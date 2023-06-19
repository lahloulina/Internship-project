using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFE.Migrations
{
    public partial class PFE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collabs",
                columns: table => new
                {
                    id_Collab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anciennete_SQLI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anciennete_Travail = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nbr_Prj_act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collabs", x => x.id_Collab);
                });

            migrationBuilder.CreateTable(
                name: "Objectifs",
                columns: table => new
                {
                    id_Objectif = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label_Obj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom_Collaborateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notation_Globale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rmq = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectifs", x => x.id_Objectif);
                });

            migrationBuilder.CreateTable(
                name: "Projets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_Projet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Membre_Equipe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Debut = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollabProjets",
                columns: table => new
                {
                    IdCollabProjet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Collab = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: true),
                    IdNavigationId = table.Column<int>(type: "int", nullable: true),
                    id_CollabNavigationid_Collab = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabProjets", x => x.IdCollabProjet);
                    table.ForeignKey(
                        name: "FK_CollabProjets_Collabs_id_CollabNavigationid_Collab",
                        column: x => x.id_CollabNavigationid_Collab,
                        principalTable: "Collabs",
                        principalColumn: "id_Collab");
                    table.ForeignKey(
                        name: "FK_CollabProjets_Projets_IdNavigationId",
                        column: x => x.IdNavigationId,
                        principalTable: "Projets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabProjets_id_CollabNavigationid_Collab",
                table: "CollabProjets",
                column: "id_CollabNavigationid_Collab");

            migrationBuilder.CreateIndex(
                name: "IX_CollabProjets_IdNavigationId",
                table: "CollabProjets",
                column: "IdNavigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabProjets");

            migrationBuilder.DropTable(
                name: "Objectifs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Collabs");

            migrationBuilder.DropTable(
                name: "Projets");
        }
    }
}
