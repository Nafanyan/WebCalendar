﻿// <auto-generated />
using System;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfrastructureMigration.Migrations
{
    [DbContext( typeof( WebCalendarDbContext ) )]
    partial class WebCalendarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel( ModelBuilder modelBuilder )
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation( "ProductVersion", "7.0.5" )
                .HasAnnotation( "Relational:MaxIdentifierLength", 63 );

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns( modelBuilder );

            modelBuilder.Entity( "Domain.Entities.Event", b =>
                {
                    b.Property<long>( "UserId" )
                        .HasColumnType( "bigint" );

                    b.Property<DateTime>( "StartEvent" )
                        .HasColumnType( "timestamp without time zone" );

                    b.Property<DateTime>( "EndEvent" )
                        .HasColumnType( "timestamp without time zone" );

                    b.Property<string>( "Description" )
                        .IsRequired()
                        .HasMaxLength( 200 )
                        .HasColumnType( "character varying(200)" );

                    b.Property<string>( "Name" )
                        .IsRequired()
                        .HasMaxLength( 100 )
                        .HasColumnType( "character varying(100)" );

                    b.HasKey( "UserId", "StartEvent", "EndEvent" );

                    b.ToTable( "Event" );
                } );

            modelBuilder.Entity( "Domain.Entities.User", b =>
                {
                    b.Property<long>( "Id" )
                        .ValueGeneratedOnAdd()
                        .HasColumnType( "bigint" );

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn( b.Property<long>( "Id" ) );

                    b.Property<string>( "Login" )
                        .IsRequired()
                        .HasMaxLength( 100 )
                        .HasColumnType( "character varying(100)" );

                    b.Property<string>( "PasswordHash" )
                        .IsRequired()
                        .HasMaxLength( 100 )
                        .HasColumnType( "character varying(100)" );

                    b.HasKey( "Id" );

                    b.ToTable( "Users", ( string )null );
                } );

            modelBuilder.Entity( "Domain.Entities.UserAuthorizationToken", b =>
                {
                    b.Property<long>( "UserId" )
                        .HasColumnType( "bigint" );

                    b.Property<DateTime>( "ExpiryDate" )
                        .HasColumnType( "timestamp without time zone" );

                    b.Property<string>( "RefreshToken" )
                        .IsRequired()
                        .HasColumnType( "text" );

                    b.HasKey( "UserId" );

                    b.ToTable( "UserAuthorizationTokens", ( string )null );
                } );

            modelBuilder.Entity( "Domain.Entities.Event", b =>
                {
                    b.HasOne( "Domain.Entities.User", null )
                        .WithMany()
                        .HasForeignKey( "UserId" )
                        .OnDelete( DeleteBehavior.Cascade )
                        .IsRequired();
                } );

            modelBuilder.Entity( "Domain.Entities.UserAuthorizationToken", b =>
                {
                    b.HasOne( "Domain.Entities.User", null )
                        .WithOne()
                        .HasForeignKey( "Domain.Entities.UserAuthorizationToken", "UserId" )
                        .OnDelete( DeleteBehavior.Cascade )
                        .IsRequired();
                } );
#pragma warning restore 612, 618
        }
    }
}
