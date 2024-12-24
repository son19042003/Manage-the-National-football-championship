using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Models;

public partial class FootballManagementContext : DbContext
{
    public FootballManagementContext()
    {
    }

    public FootballManagementContext(DbContextOptions<FootballManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    public virtual DbSet<Standing> Standings { get; set; }

    public virtual DbSet<TypeGoal> TypeGoals { get; set; }

    public virtual DbSet<TypeNews> TypeNews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=FootballManagement;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.HasIndex(e => e.Email, "UQ__Account__AB6E616415CCCC07").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.Gender)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phoneNum");
            entity.Property(e => e.RandomKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("randomKey");
            entity.Property(e => e.ResetKeyExpires)
                .HasColumnType("datetime")
                .HasColumnName("resetKeyExpires");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.ToTable("Club");

            entity.Property(e => e.ClubId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("clubId");
            entity.Property(e => e.ClubName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("clubName");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.LinkFb)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("linkFb");
            entity.Property(e => e.LinkIg)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("linkIg");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("logo");
            entity.Property(e => e.ShortName)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("shortName");
            entity.Property(e => e.Stadium)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("stadium");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("PK__Goals__7E225EB1A068B213");

            entity.Property(e => e.GoalId).HasColumnName("goalId");
            entity.Property(e => e.GoalIndex).HasDefaultValue(1);
            entity.Property(e => e.MatchId).HasColumnName("matchId");
            entity.Property(e => e.PlayerId).HasColumnName("playerId");
            entity.Property(e => e.TeamId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("teamId");
            entity.Property(e => e.TimeScored)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("timeScored");
            entity.Property(e => e.TypeGid).HasColumnName("typeGId");

            entity.HasOne(d => d.Match).WithMany(p => p.Goals)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Goals__matchId__3C34F16F");

            entity.HasOne(d => d.Player).WithMany(p => p.GoalsNavigation)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Goals__playerId__3D2915A8");

            entity.HasOne(d => d.Team).WithMany(p => p.Goals)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Goals__teamId__3E1D39E1");

            entity.HasOne(d => d.TypeG).WithMany(p => p.Goals)
                .HasForeignKey(d => d.TypeGid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Goals__typeGId__3F115E1A");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.Property(e => e.MatchId).HasColumnName("matchId");
            entity.Property(e => e.AwayTeam)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("awayTeam");
            entity.Property(e => e.DateStart).HasColumnName("dateStart");
            entity.Property(e => e.GoalsA)
                .HasDefaultValue(0)
                .HasColumnName("goalsA");
            entity.Property(e => e.GoalsH)
                .HasDefaultValue(0)
                .HasColumnName("goalsH");
            entity.Property(e => e.HomeTeam)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("homeTeam");
            entity.Property(e => e.Round).HasColumnName("round");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TimeStart).HasColumnName("timeStart");

            entity.HasOne(d => d.AwayTeamNavigation).WithMany(p => p.MatchAwayTeamNavigations)
                .HasForeignKey(d => d.AwayTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matches_AwayTeam");

            entity.HasOne(d => d.HomeTeamNavigation).WithMany(p => p.MatchHomeTeamNavigations)
                .HasForeignKey(d => d.HomeTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matches_HomeTeam");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.NewsId).HasColumnName("newsId");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.DateU)
                .HasColumnType("datetime")
                .HasColumnName("dateU");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.ImgContent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imgContent");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.TypeNewsId).HasColumnName("typeNewsId");

            entity.HasOne(d => d.TypeNews).WithMany(p => p.News)
                .HasForeignKey(d => d.TypeNewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_News_TypeNews");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.PlayerId).HasColumnName("playerId");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.ClubId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("clubId");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.Goals)
                .HasDefaultValue(0)
                .HasColumnName("goals");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.IsInClub)
                .HasDefaultValue(true)
                .HasColumnName("isInClub");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.LinkFb)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("linkFb");
            entity.Property(e => e.LinkIg)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("linkIg");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nationality");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.TypePlayer).HasColumnName("typePlayer");

            entity.HasOne(d => d.Club).WithMany(p => p.Players)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Players_Club");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RoleDes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("roleDes");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.ToTable("Rule");

            entity.Property(e => e.RuleId).HasColumnName("ruleId");
            entity.Property(e => e.MaxForeignPlayers).HasColumnName("maxForeignPlayers");
            entity.Property(e => e.MaxPlayer).HasColumnName("maxPlayer");
            entity.Property(e => e.MinAge).HasColumnName("minAge");
            entity.Property(e => e.MinPlayer).HasColumnName("minPlayer");
        });

        modelBuilder.Entity<Standing>(entity =>
        {
            entity.ToTable("Standing");

            entity.Property(e => e.StandingId).HasColumnName("standingId");
            entity.Property(e => e.ClubId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("clubId");
            entity.Property(e => e.Drawn)
                .HasDefaultValue(0)
                .HasColumnName("drawn");
            entity.Property(e => e.GoalA)
                .HasDefaultValue(0)
                .HasColumnName("goalA");
            entity.Property(e => e.GoalF)
                .HasDefaultValue(0)
                .HasColumnName("goalF");
            entity.Property(e => e.Lost)
                .HasDefaultValue(0)
                .HasColumnName("lost");
            entity.Property(e => e.Played)
                .HasDefaultValue(0)
                .HasColumnName("played");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.Won)
                .HasDefaultValue(0)
                .HasColumnName("won");

            entity.HasOne(d => d.Club).WithMany(p => p.Standings)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Standing_Club");
        });

        modelBuilder.Entity<TypeGoal>(entity =>
        {
            entity.HasKey(e => e.TypeGid);

            entity.ToTable("TypeGoal");

            entity.Property(e => e.TypeGid).HasColumnName("typeGId");
            entity.Property(e => e.TypeGdes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("typeGDes");
            entity.Property(e => e.TypeGname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("typeGName");
        });

        modelBuilder.Entity<TypeNews>(entity =>
        {
            entity.Property(e => e.TypeNewsId).HasColumnName("typeNewsId");
            entity.Property(e => e.TypeNewsDes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("typeNewsDes");
            entity.Property(e => e.TypeNewsName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("typeNewsName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
