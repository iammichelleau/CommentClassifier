import { Comment } from './comment';

class CommentService {
  private commentsUrl = 'http://localhost:64859/api/comments';

  async submitComment(comment: Comment): Promise<string> {
    const response = await fetch(this.commentsUrl, {
      method: 'post',
      body: JSON.stringify(comment),
      headers: {
        'Content-Type': 'application/json'
      }
    });

    const text = await response.text();
    return text;
  }
}

export default new CommentService();