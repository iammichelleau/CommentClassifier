import { Component, OnInit } from '@angular/core';
import { Comment } from '../comment'
import CommentService  from '../comment.service'

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

  comments: Comment[];
  response: string = null;

  constructor() { }

  ngOnInit() {
    // this.getComments();
  }

  async submit(Content: string) {
    Content = Content.trim();
    if (!Content) { return; }
    this.response = await CommentService.submitComment({ Content } as Comment);
  }

}
