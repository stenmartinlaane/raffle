﻿using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ActivityEvent = App.Domain.ActivityEvent;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>

{
    public DbSet<ActivityEvent> ActivityEvents { get; set; } = default!;
    public DbSet<MinutesAdded> MinutesAddeds { get; set; } = default!;
    public DbSet<ParticipantEvent> ParticipantEvents { get; set; } = default!;
    public DbSet<RaffleItem> RaffleItems { get; set; } = default!;
    
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (var prop in entity
                         .Properties
                         .Where(x => x.Metadata.ClrType == typeof(DateTime)))
            {
                prop.CurrentValue = ((DateTime) prop.CurrentValue).ToUniversalTime();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}