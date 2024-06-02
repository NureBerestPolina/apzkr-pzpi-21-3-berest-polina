import { Component, OnInit } from '@angular/core';
import { StatisticsService } from '../../../services/statistics/statistics.service';
import { GetStatisticsResponse } from '../../../models/dtos/statistics-response.model';

@Component({
  selector: 'app-system-statistics',
  templateUrl: './system-statistics.component.html',
  styleUrl: './system-statistics.component.css'
})
export class SystemStatisticsComponent implements OnInit {
  organizationsStatistics: any[] | undefined;

  constructor(private statisticsService: StatisticsService) { }

  ngOnInit(): void {
    this.statisticsService.getAllOrganizationsStatistics().subscribe(data => {
      this.organizationsStatistics = data.map(item => ({
        ...item,
        monthlyCharge: item.totalSalesSum * 0.05
      }));
    });
  }

}
