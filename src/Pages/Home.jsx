import React, { useContext } from 'react';
import { context } from '../Context/Store';

function Home() {
  const { posts } = useContext(context);

  return (
    <div>
      <h2>Posts</h2>
      {posts.length === 0 ? (
        <p>No posts found.</p>
      ) : (
        posts.map((post, index) => (
          <div key={index} style={{ border: '1px solid #ccc', padding: '10px', marginBottom: '10px' }}>
            {post.postPicUrl && (
              <img
                src={post.postPicUrl}
                alt="Post"
                style={{ width: '100%', maxHeight: '300px', objectFit: 'cover' }}
              />
            )}
            <h3>{post.caption}</h3>
            <p>Tags: {post.tags.join(', ')}</p>
            <small>Posted by: {post.userId}</small><br />
            <small>Created: {new Date(post.createdAt).toLocaleString()}</small>
          </div>
        ))
      )}
    </div>
  );
}

export default Home;
