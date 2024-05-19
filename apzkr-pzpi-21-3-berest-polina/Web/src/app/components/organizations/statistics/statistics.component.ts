import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StatisticsService } from '../../../services/statistics/statistics.service';
import { GetOrganizationStatisticsResponse } from '../../../models/dtos/organization-statistics-response.model';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrl: './statistics.component.css'
})
export class StatisticsComponent implements OnInit{
  organizationId!: string;
  organizationStatistics?: GetOrganizationStatisticsResponse[];

  constructor(
    private route: ActivatedRoute, 
    private statisticsService: StatisticsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const organizationId = this.route.snapshot.paramMap.get('organizationId');

    if (!organizationId) {
      this.router.navigateByUrl('/notFound');
      return;
    }

    this.organizationId = organizationId;
    this.fetchOrganizationStatistics();
  }

  fetchOrganizationStatistics(): void {
    this.statisticsService.getOrganizationStatistics(this.organizationId).subscribe(data => {
      this.organizationStatistics = data;
    });
  }
}
